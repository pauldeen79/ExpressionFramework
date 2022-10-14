namespace ExpressionFramework.Domain;

public static class IntExpression
{
    public static IEnumerable<ValidationResult> ValidateParameter(object? context, Expression expression, string name)
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
