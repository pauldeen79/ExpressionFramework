namespace ExpressionFramework.Domain.Expressions;

public partial record ToLowerCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(s.ToLower())
            : Result<object?>.Invalid("Context must be of type string");

    public override IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
    {
        if (context is not string)
        {
            yield return new ValidationResult("Context must be of type string");
        }
    }
}
