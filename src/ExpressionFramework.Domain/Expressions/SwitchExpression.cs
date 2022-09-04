namespace ExpressionFramework.Domain.Expressions;

public partial record SwitchExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Cases == null)
        {
            yield return new ValidationResult($"{nameof(Cases)} is required", new[] { nameof(Cases) });
        }

        if (DefaultExpression == null)
        {
            yield return new ValidationResult($"{nameof(DefaultExpression)} is required", new[] { nameof(DefaultExpression) });
        }
    }
}
