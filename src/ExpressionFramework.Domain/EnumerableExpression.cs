namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(
        Expression expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var enumerableResult = expression.EvaluateTyped<IEnumerable>(context, "Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(enumerableResult);
        }

        var results = @delegate(enumerableResult.Value!.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return results.Last();
        }

        return Result<object?>.Success(results.Select(x => x.Value));
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(
        Expression expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var enumerableResult = expression.EvaluateTyped<IEnumerable>(context, "Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(enumerableResult);
        }

        var results = @delegate(enumerableResult.Value!.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(results.Last());
        }

        return Result<IEnumerable<object?>>.Success(results.Select(x => x.Value));
    }

    public static Result<T> GetRequiredScalarValue<T>(object? context,
                                                      Expression enumerableExpression,
                                                      Expression? predicateExpression,
                                                      Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                                      Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate = null,
                                                      Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                                      bool predicateIsRequired = false)
        => GetScalarValue
        (
            context,
            enumerableExpression,
            predicateExpression,
            delegateWithoutPredicate,
            delegateWithPredicate,
            _ => Result<T>.Invalid("Enumerable is empty"),
            _ => Result<T>.Invalid("None of the items conform to the supplied predicate"),
            selectorDelegate,
            predicateIsRequired
        );

#pragma warning disable S107 // Methods should not have too many parameters
    public static Result<T> GetOptionalScalarValue<T>(object? context,
                                                      Expression enumerableExpression,
                                                      Expression? predicateExpression,
                                                      Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                                      Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate = null,
                                                      Func<object?, Result<T>>? defaultValueDelegate = null,
                                                      Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                                      bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
        => GetScalarValue
        (
            context,
            enumerableExpression,
            predicateExpression,
            delegateWithoutPredicate,
            delegateWithPredicate,
            defaultValueDelegate,
            defaultValueDelegate,
            selectorDelegate,
            predicateIsRequired
        );

    public static Result<T> GetAggregateValue<T>(object? context,
                                                 Expression enumerableExpression,
                                                 Func<IEnumerable<object?>, Result<T>> aggregateDelegate,
                                                 Expression? selectorExpression = null)
        => GetTypedResultFromEnumerable(enumerableExpression, context, x => x
            .Select(y => selectorExpression == null
                ? Result<object?>.Success(y)
                : selectorExpression.Evaluate(y))).Transform(result => result.IsSuccessful()
                    ? aggregateDelegate.Invoke(result.Value!)
                    : Result<T>.FromExistingResult(result));

    public static Result<object?> GetInvalidResult(object? context)
        => GetInvalidResult<object?>(context);

    public static Result<T> GetInvalidResult<T>(object? context)
        => context.Transform(x => Result<T>.Invalid("Expression is not of type enumerable"));
    
    public static Result<object?> GetDefaultValue(Expression? defaultExpression, object? context)
        => defaultExpression == null
            ? new EmptyExpression().Evaluate(context)
            : defaultExpression.Evaluate(context);

    public static IEnumerable<ValidationResult> ValidateEmptyPredicate(Expression? predicateExpression)
    {
        if (predicateExpression == null)
        {
            yield return new ValidationResult("Predicate is required");
        }
    }

#pragma warning disable S107 // Methods should not have too many parameters
    public static ExpressionDescriptor GetDescriptor(Type type,
                                                     string description,
                                                     string okValue,
                                                     string okDescription,
                                                     string invalidDescription,
                                                     string errorDescription,
                                                     bool hasDefaultExpression,
                                                     Type resultValueType,
                                                     bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            null,
            null,
            null,
            new[]
            {
                new ParameterDescriptor("Expression", typeof(IEnumerable).FullName, "Enumerable expression to use", true),
                new ParameterDescriptor("PredicateExpression", typeof(bool).FullName, predicateIsRequired ? "Predicate to use" : "Optional predicate to use", predicateIsRequired),
                new ParameterDescriptor("DefaultExpression", typeof(object).FullName, "Optional default value to use", false),
            }.Where(x => x.Name != "DefaultExpression" || hasDefaultExpression),
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, resultValueType, okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", null, invalidDescription),
                new ReturnValueDescriptor(ResultStatus.Error, "Empty", null, errorDescription),
            });

#pragma warning disable S107 // Methods should not have too many parameters
    private static Result<T> GetScalarValue<T>(object? context,
                                               Expression enumerableExpression,
                                               Expression? predicateExpression,
                                               Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                               Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithoutPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithPredicate,
                                               Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                               bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
    {
        var enumerableResult = enumerableExpression.Evaluate(context).TryCast<IEnumerable>("Expression is not of type enumerable");
        if (!enumerableResult.IsSuccessful())
        {
            return Result<T>.FromExistingResult(enumerableResult);
        }

        if (predicateIsRequired && predicateExpression == null)
        {
            return Result<T>.Invalid("Predicate is required");
        }

        selectorDelegate ??= new Func<IEnumerable<object?>, Result<IEnumerable<object?>>>(x => Result<IEnumerable<object?>>.Success(x));

        var itemsResult = selectorDelegate.Invoke(enumerableResult.Value.OfType<object?>());
        if (!itemsResult.IsSuccessful())
        {
            return Result<T>.FromExistingResult(itemsResult);
        }

        if (predicateExpression == null)
        {
            if (!itemsResult.Value.Any() && defaultValueDelegateWithoutPredicate != null)
            {
                return defaultValueDelegateWithoutPredicate.Invoke(context);
            }

            return delegateWithoutPredicate.Invoke(itemsResult.Value!);
        }

        var results = itemsResult.Value.Select(x => new ItemResult
        (
            x,
            predicateExpression.EvaluateTyped<bool>(x, "Predicate did not return a boolean value")
        )).TakeWhileWithFirstNonMatching(x => x.Result.IsSuccessful());

        if (results.Any(x => !x.Result.IsSuccessful()))
        {
            // Error in predicate evaluation
            return Result<T>.FromExistingResult(results.First(x => !x.Result.IsSuccessful()).Result);
        }

        if (!results.Any(x => x.Result.Value) && defaultValueDelegateWithPredicate != null)
        {
            return defaultValueDelegateWithPredicate.Invoke(context);
        }

        return delegateWithPredicate?.Invoke(results)
            ?? Result<T>.Invalid("DelegateWithPredicate is required when predicate is filled");
    }
}
