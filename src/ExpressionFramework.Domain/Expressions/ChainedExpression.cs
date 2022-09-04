namespace ExpressionFramework.Domain.Expressions;

public partial record ChainedExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Expressions == null)
        {
            yield return new ValidationResult($"{nameof(Expressions)} is required", new[] { nameof(Expressions) });
        }
    }
}
