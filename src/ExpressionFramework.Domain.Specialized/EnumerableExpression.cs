namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(
        ITypedExpression<IEnumerable> expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var enumerableResult = expression.EvaluateTyped(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(enumerableResult);
        }

        if (enumerableResult.Value == null)
        {
            return Result<object?>.Invalid("Expression is not of type enumerable");
        }

        var results = @delegate(enumerableResult.Value.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
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

        if (enumerableResult.Value == null)
        {
            return Result<IEnumerable<object?>>.Invalid("Expression is not of type enumerable");
        }

        return GetTypedResultFromEnumerable(enumerableResult.Value, @delegate);
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(
        ITypedExpression<IEnumerable> expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var enumerableResult = expression.EvaluateTyped(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(enumerableResult);
        }

        if (enumerableResult.Value == null)
        {
            return Result<IEnumerable<object?>>.Invalid("Expression is not of type enumerable");
        }

        return GetTypedResultFromEnumerable(enumerableResult.Value, @delegate);
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerableWithCount(
        ITypedExpression<IEnumerable> expression,
        ITypedExpression<int> countExpression,
        object? context,
        Func<IEnumerable<object?>, Result<int>, IEnumerable<Result<object?>>> @delegate)
    {
        var countResult = countExpression.EvaluateTyped(context);
        if (!countResult.IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(countResult);
        }

        return GetTypedResultFromEnumerable(expression, context, e => @delegate(e, countResult));

    }

    private static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(
        IEnumerable enumerable,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        if (enumerable == null)
        {
            return Result<IEnumerable<object?>>.Invalid("Expression is not of type enumerable");
        }

        var results = @delegate(enumerable.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(results.Last());
        }

        return Result<IEnumerable<object?>>.Success(results.Select(x => x.Value));
    }

    public static Result<T> GetRequiredScalarValue<T>(object? context,
                                                      ITypedExpression<IEnumerable> enumerableExpression,
                                                      TypedDelegateExpression<bool>? predicateExpression,
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
                                                      ITypedExpression<IEnumerable> enumerableExpression,
                                                      TypedDelegateExpression<bool>? predicateExpression,
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
                                                 ITypedExpression<IEnumerable> enumerableExpression,
                                                 Func<IEnumerable<object?>, Result<T>> aggregateDelegate,
                                                 Expression? selectorExpression = null)
        => GetTypedResultFromEnumerable(enumerableExpression, context, x => x
            .Select(y => selectorExpression is null
                ? Result<object?>.Success(y)
                : selectorExpression.Evaluate(y))).Transform(result => result.IsSuccessful()
                    ? result.Transform(x => x.Value == null
                        ? Result<T>.Invalid("Expression is not of type enumerable")
                        : aggregateDelegate.Invoke(x.Value))
                    : Result<T>.FromExistingResult(result));

    public static Result<object?> GetDefaultValue(Expression? defaultExpression, object? context)
        => defaultExpression is null
            ? new EmptyExpression().Evaluate(context)
            : defaultExpression.Evaluate(context);

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
                                               ITypedExpression<IEnumerable> enumerableExpression,
                                               TypedDelegateExpression<bool>? predicateExpression,
                                               Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                               Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithoutPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithPredicate,
                                               Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                               bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
    {
        var enumerableResult = enumerableExpression.EvaluateTyped(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result<T>.FromExistingResult(enumerableResult);
        }

        if (predicateIsRequired && predicateExpression is null)
        {
            return Result<T>.Invalid("Predicate is required");
        }

        if (enumerableResult.Value == null)
        {
            return Result<T>.Invalid("Expression is not of type enumerable");
        }

        selectorDelegate ??= new Func<IEnumerable<object?>, Result<IEnumerable<object?>>>(x => Result<IEnumerable<object?>>.Success(x));
        var itemsResult = selectorDelegate.Invoke(enumerableResult.Value.OfType<object?>());
        if (!itemsResult.IsSuccessful())
        {
            return Result<T>.FromExistingResult(itemsResult);
        }

        if (predicateExpression is null)
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
            predicateExpression.EvaluateTyped<bool>(x)
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
