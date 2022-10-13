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

    public static IEnumerable<ValidationResult> ValidateContext(object? context, Func<IEnumerable<ValidationResult>>? additionalValidationErrorsDelegate = null)
    {
        if (context == null)
        {
            yield return new ValidationResult("Context cannot be empty");
            yield break;
        }

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

    public static IEnumerable<ValidationResult> IntExpressionValidation(object? context, Expression expression, string name)
    {
        if (context is not IEnumerable e)
        {
            yield break;
        }

        var countResult = expression.Evaluate(context);
        if (countResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"{name} returned an invalid result. Error message: {countResult.ErrorMessage}");
        }
        else if (countResult.Status == ResultStatus.Ok && countResult.Value is not int)
        {
            yield return new ValidationResult($"{name} did not return an integer");
        }
    }
}
