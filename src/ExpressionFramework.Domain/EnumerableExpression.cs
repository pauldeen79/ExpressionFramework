namespace ExpressionFramework.Domain;

public static class EnumerableExpression
{
    public static Result<object?> GetResultFromEnumerable(IEnumerable e, Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(e.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return results.Last();
        }

        return Result<object?>.Success(results.Select(x => x.Value));
    }

    public static Result<IEnumerable<object?>> GetTypedResultFromEnumerable(IEnumerable e, Func<IEnumerable<object?>, IEnumerable<Result<object?>>> @delegate)
    {
        var results = @delegate(e.OfType<object?>()).TakeWhileWithFirstNonMatching(x => x.IsSuccessful()).ToArray();
        if (!results.Last().IsSuccessful())
        {
            return Result<IEnumerable<object?>>.FromExistingResult(results.Last());
        }

        return Result<IEnumerable<object?>>.Success(results.Select(x => x.Value));
    }

    public static Result<object?> GetScalarValueWithoutDefault(object? context,
                                                               Expression? predicate,
                                                               Func<IEnumerable<object?>, object?> delegateWithoutPredicate,
                                                               Func<IEnumerable<ItemResult>, object?> delegateWithPredicate,
                                                               Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            _ => Result<object?>.Invalid("Enumerable is empty"),
            _ => Result<object?>.Invalid("None of the items conform to the supplied predicate"),
            selectorDelegate
        );

    public static Result<object?> GetScalarValueWithDefault(object? context,
                                                            Expression? predicate,
                                                            Func<IEnumerable<object?>, object?> delegateWithoutPredicate,
                                                            Func<IEnumerable<ItemResult>, object?> delegateWithPredicate,
                                                            Func<object?, Result<object?>> defaultValueDelegate,
                                                            Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            defaultValueDelegate,
            defaultValueDelegate,
            selectorDelegate
        );

    private static Result<object?> GetScalarValue(object? context,
                                                  Expression? predicate,
                                                  Func<IEnumerable<object?>, object?> delegateWithoutPredicate,
                                                  Func<IEnumerable<ItemResult>, object?> delegateWithPredicate,
                                                  Func<object?, Result<object?>>? defaultValueDelegateWithoutPredicate,
                                                  Func<object?, Result<object?>>? defaultValueDelegateWithPredicate,
                                                  Func<IEnumerable<object?>, Result<IEnumerable<object?>>>? selectorDelegate = null)
    {
        if (context is not IEnumerable e)
        {
            return Result<object?>.Invalid("Context is not of type enumerable");
        }

        if (selectorDelegate == null)
        {
            selectorDelegate = new Func<IEnumerable<object?>, Result<IEnumerable<object?>>>(x => Result<IEnumerable<object?>>.Success(x));
        }

        var itemsResult = selectorDelegate.Invoke(e.OfType<object?>());
        if (!itemsResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(itemsResult);
        }

        if (predicate == null)
        {
            if (!itemsResult.Value.Any() && defaultValueDelegateWithoutPredicate != null)
            {
                return defaultValueDelegateWithoutPredicate.Invoke(context);
            }

            return Result<object?>.Success(delegateWithoutPredicate.Invoke(itemsResult.Value!));
        }

        var results = itemsResult.Value.Select(x => new ItemResult
        (
            x,
            predicate.Evaluate(x).TryCast<bool>("Predicate did not return a boolean value")
        )).TakeWhileWithFirstNonMatching(x => x.Result.IsSuccessful());

        if (results.Any(x => !x.Result.IsSuccessful()))
        {
            // Error in predicate evaluation
            return Result<object?>.FromExistingResult(results.First(x => !x.Result.IsSuccessful()).Result);
        }

        if (!results.Any(x => x.Result.Value) && defaultValueDelegateWithPredicate != null)
        {
            return defaultValueDelegateWithPredicate.Invoke(context);
        }

        return Result<object?>.Success(delegateWithPredicate.Invoke(results));
    }

    public static Result<object?> GetDefaultValue(Expression? defaultExpression, object? context)
        => defaultExpression == null
            ? new EmptyExpression().Evaluate(context)
            : defaultExpression.Evaluate(context);

    public static IEnumerable<ValidationResult> ValidateContext(object? context, Func<IEnumerable<ValidationResult>>? additionalValidationErrorsDelegate = null)
    {
        if (context is not IEnumerable e)
        {
            yield return new ValidationResult("Context must be of type IEnumerable");
        }

        if (additionalValidationErrorsDelegate != null)
        {
            foreach (var error in additionalValidationErrorsDelegate.Invoke())
            {
                yield return error;
            }
        }
    }

    public static IEnumerable<ValidationResult> ValidateEmptyEnumerable(object? context)
    {
        if (context is not IEnumerable e)
        {
            yield break;
        }

        if (!e.OfType<object?>().Any())
        {
            yield return new ValidationResult("Enumerable is empty");
        }
    }

    public static ExpressionDescriptor GetDescriptor(Type type, string description, string okValue, string okDescription, string invalidDescription, string errorDescription, bool hasDefaultExpression)
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            typeof(IEnumerable).FullName,
            "Enumerable value to use",
            true,
            new[]
            {
                new ParameterDescriptor("PredicateExpression", typeof(Expression).FullName, "Optional predicate to use", false),
                new ParameterDescriptor("DefaultExpression", typeof(Expression).FullName, "Optional default value to use", false),
            }.Where(x => x.Name != "DefaultExpression" || hasDefaultExpression),
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(object), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", typeof(object), invalidDescription),
                new ReturnValueDescriptor(ResultStatus.Error, "Empty", typeof(object), errorDescription),
            });
}
