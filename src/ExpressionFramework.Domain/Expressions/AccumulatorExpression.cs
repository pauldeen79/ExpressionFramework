namespace ExpressionFramework.Domain.Expressions;

public partial record AccumulatorExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Expression == null)
        {
            yield return new ValidationResult($"{nameof(Expression)} is required", new[] { nameof(Expression) });
        }
    }
}
