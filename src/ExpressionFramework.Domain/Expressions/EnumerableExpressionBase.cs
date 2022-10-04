namespace ExpressionFramework.Domain.Expressions;

public static class EnumerableExpressionBase
{
    public static IEnumerable<ValidationResult> ValidateContext(object? context)
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
    }
}
