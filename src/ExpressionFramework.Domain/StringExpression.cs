namespace ExpressionFramework.Domain;

public static class StringExpression
{
    public static IEnumerable<ValidationResult> ValidateContext(object? context)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }
    }
}
