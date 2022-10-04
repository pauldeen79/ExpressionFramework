namespace ExpressionFramework.Domain.Expressions;

public static class StringExpressionBase
{
    public static IEnumerable<ValidationResult> ValidateContext(object? context)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }
    }
}
