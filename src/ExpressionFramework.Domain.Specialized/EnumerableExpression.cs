namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(
        ITypedExpression<IEnumerable> expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
        => GetResultFromEnumerable(expression, context, default, @delegate);

    public static Result<object?> GetResultFromEnumerable(
        ITypedExpression<IEnumerable> expression,
        object? context,
        string? errorMessage,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        if (expression is null)
        {
            return Result.Invalid<object?>("Expression is required");
        }

        if (@delegate is null)
        {
            return Result.Invalid<object?>("Delegate is required");
        }

        var enumerableResult = expression.EvaluateTypedWithTypeCheck(context, errorMessage);
        if (!enumerableResult.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(enumerableResult);
        }

        var results = @delegate.Invoke(enumerableResult.Value.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results[results.Length - 1].IsSuccessful())
        {
            return results[results.Length - 1];
        }

        return Result.Success<object?>(results.Select(x => x.Value));
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(
        ITypedExpression<IEnumerable> expression,
        object? context,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        if (expression is null)
        {
            return Result.Invalid<IEnumerable<object?>>("Expression is required");
        }

        if (@delegate is null)
        {
            return Result.Invalid<IEnumerable<object?>>("Delegate is required");
        }

        var enumerableResult = expression.EvaluateTypedWithTypeCheck(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(enumerableResult);
        }

        return GetTypedResultFromEnumerable(enumerableResult.Value!, @delegate);
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerableWithCount(
        ITypedExpression<IEnumerable> expression,
        ITypedExpression<int> countExpression,
        object? context,
        Func<IEnumerable<object?>, Result<int>, IEnumerable<Result<object?>>> @delegate)
    {
        if (countExpression is null)
        {
            return Result.Invalid<IEnumerable<object?>>("Count expression is required");
        }

        if (@delegate is null)
        {
            return Result.Invalid<IEnumerable<object?>>("Delegate is required");
        }

        var countResult = countExpression.EvaluateTyped(context);
        if (!countResult.IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(countResult);
        }

        return GetTypedResultFromEnumerable(expression, context, e => @delegate(e, countResult));
    }

    private static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(
        IEnumerable enumerable,
        Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(enumerable.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results[results.Length - 1].IsSuccessful())
        {
            return Result.FromExistingResult<IEnumerable<object?>>(results[results.Length - 1]);
        }

        return Result.Success(results.Select(x => x.Value));
    }

    public static Result<T> GetRequiredScalarValue<T>(object? context,
                                                      ITypedExpression<IEnumerable> enumerableExpression,
                                                      ITypedExpression<bool>? predicateExpression,
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
            _ => Result.Invalid<T>("Enumerable is empty"),
            _ => Result.Invalid<T>("None of the items conform to the supplied predicate"),
            selectorDelegate,
            predicateIsRequired
        );

#pragma warning disable S107 // Methods should not have too many parameters
    public static Result<T> GetOptionalScalarValue<T>(object? context,
                                                      ITypedExpression<IEnumerable> enumerableExpression,
                                                      ITypedExpression<bool>? predicateExpression,
                                                      Func<IEnumerable<object?>, Result<T>> delegateWithoutPredicate,
                                                      Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate = null,
                                                      Func<object?, Result<T>>? defaultValueDelegate = null,
                                                      Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                                      bool predicateIsRequired = false)
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
                ? Result.Success(y)
                : selectorExpression.EvaluateWithNullCheck(y))).Transform(result => result.IsSuccessful()
                    ? aggregateDelegate.Invoke(result.Value!)
                    : Result.FromExistingResult<T>(result));

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
                                                     Type? resultValueType,
                                                     bool predicateIsRequired = false)
    {
        type = ArgumentGuard.IsNotNull(type, nameof(type));

        return new(
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
    }

#pragma warning disable S107 // Methods should not have too many parameters
    private static Result<T> GetScalarValue<T>(object? context,
                                               ITypedExpression<IEnumerable> enumerableExpression,
                                               ITypedExpression<bool>? predicateExpression,
                                               Func<IEnumerable<object?>, Result<T>>? delegateWithoutPredicate,
                                               Func<IEnumerable<ItemResult>, Result<T>>? delegateWithPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithoutPredicate,
                                               Func<object?, Result<T>>? defaultValueDelegateWithPredicate,
                                               Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null,
                                               bool predicateIsRequired = false)
#pragma warning restore S107 // Methods should not have too many parameters
    {
        var enumerableResult = enumerableExpression.EvaluateTypedWithTypeCheck(context);
        if (!enumerableResult.IsSuccessful())
        {
            return Result.FromExistingResult<T>(enumerableResult);
        }

        if (predicateIsRequired && predicateExpression is null)
        {
            return Result.Invalid<T>("Predicate is required");
        }

        selectorDelegate ??= new Func<IEnumerable<object?>, Result<IEnumerable<object?>>>(Result.Success);
        var itemsResult = selectorDelegate.Invoke(enumerableResult.Value.OfType<object?>());
        if (!itemsResult.IsSuccessful())
        {
            return Result.FromExistingResult<T>(itemsResult);
        }

        if (predicateExpression is null)
        {
            if (!itemsResult.Value.Any() && defaultValueDelegateWithoutPredicate != null)
            {
                return defaultValueDelegateWithoutPredicate.Invoke(context);
            }

            if (delegateWithoutPredicate is null)
            {
                return Result.Invalid<T>("Delegate without predicate is required");
            }

            return delegateWithoutPredicate.Invoke(itemsResult.Value!);
        }

        var results = itemsResult.Value.Select(x => new ItemResult
        (
            x,
            predicateExpression.EvaluateTyped(x)
        )).TakeWhileWithFirstNonMatching(x => x.Result.IsSuccessful()).ToArray();

        if (Array.Exists(results, x => !x.Result.IsSuccessful()))
        {
            // Error in predicate evaluation
            return Result.FromExistingResult<T>(results.First(x => !x.Result.IsSuccessful()).Result);
        }

        if (!Array.Exists(results, x => x.Result.Value) && defaultValueDelegateWithPredicate != null)
        {
            return defaultValueDelegateWithPredicate.Invoke(context);
        }

        return delegateWithPredicate?.Invoke(results)
            ?? Result.Invalid<T>("DelegateWithPredicate is required when predicate is filled");
    }
}
