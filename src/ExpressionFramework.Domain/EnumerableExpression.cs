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
                                                               Func<IEnumerable<ItemResult>, object?> delegateWithPredicate)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            _ => Result<object?>.Invalid("Enumerable is empty"),
            _ => Result<object?>.Invalid("None of the items conform to the supplied predicate")
        );

    public static Result<object?> GetScalarValueWithDefault(object? context,
                                                            Expression? predicate,
                                                            Func<IEnumerable<object?>, object?> delegateWithoutPredicate,
                                                            Func<IEnumerable<ItemResult>, object?> delegateWithPredicate,
                                                            Func<object?, Result<object?>> defaultValueDelegate)
        => GetScalarValue
        (
            context,
            predicate,
            delegateWithoutPredicate,
            delegateWithPredicate,
            defaultValueDelegate,
            defaultValueDelegate
        );

    private static Result<object?> GetScalarValue(object? context,
                                                   Expression? predicate,
                                                   Func<IEnumerable<object?>, object?> delegateWithoutPredicate,
                                                   Func<IEnumerable<ItemResult>, object?> delegateWithPredicate,
                                                   Func<object?, Result<object?>>? defaultValueDelegateWithoutPredicate,
                                                   Func<object?, Result<object?>>? defaultValueDelegateWithPredicate)
    {
        if (context is not IEnumerable e)
        {
            return Result<object?>.Invalid("Context is not of type enumerable");
        }

        var items = e.OfType<object?>();

        if (predicate == null)
        {
            if (!items.Any() && defaultValueDelegateWithoutPredicate != null)
            {
                return defaultValueDelegateWithoutPredicate.Invoke(context);
            }

            return Result<object?>.Success(delegateWithoutPredicate.Invoke(items));
        }

        var results = items.Select(x => new ItemResult
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
            yield break;
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
        if (context is IEnumerable e && !e.OfType<object?>().Any())
        {
            yield return new ValidationResult("Enumerable is empty");
        }
    }
}
