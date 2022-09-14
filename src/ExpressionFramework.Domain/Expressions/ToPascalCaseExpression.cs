namespace ExpressionFramework.Domain.Expressions;

public partial record ToPascalCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(ToPascalCase(context?.ToString()));

    private string? ToPascalCase(string? value)
    {
        if (value != null && !string.IsNullOrEmpty(value))
        {
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        return value;
    }
}
