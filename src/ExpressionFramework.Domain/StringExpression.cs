namespace ExpressionFramework.Domain;

public static class StringExpression
{
    public static IEnumerable<ValidationResult> ValidateContext(object? context, Func<IEnumerable<ValidationResult>>? additionalValidationErrorsDelegate = null)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }

        if (additionalValidationErrorsDelegate != null)
        {
            foreach (var error in additionalValidationErrorsDelegate.Invoke())
            {
                yield return error;
            }
        }
    }

    public static IEnumerable<ValidationResult> ValidateLength(object? context, Expression lengthExpression)
    {
        if (context is not string s)
        {
            yield break;
        }

        int? localLength = null;

        var lengthResult = lengthExpression.Evaluate(context);
        if (lengthResult.Status == ResultStatus.Invalid)
        {
            yield return new ValidationResult($"LengthExpression returned an invalid result. Error message: {lengthResult.ErrorMessage}");
        }
        else if (lengthResult.Status == ResultStatus.Ok)
        {
            if (lengthResult.Value is not int length)
            {
                yield return new ValidationResult($"LengthExpression did not return an integer");
            }
            else
            {
                localLength = length;
            }
        }

        if (localLength.HasValue && s.Length < localLength)
        {
            yield return new ValidationResult("Length must refer to a location within the string");
        }
    }
}
