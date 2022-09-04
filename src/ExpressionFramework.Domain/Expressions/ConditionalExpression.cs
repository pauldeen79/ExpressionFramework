namespace ExpressionFramework.Domain.Expressions;

public partial record ConditionalExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Conditions == null)
        {
            yield return new ValidationResult($"{nameof(Conditions)} is required", new[] { nameof(Conditions) });
        }

        if (ResultExpression == null)
        {
            yield return new ValidationResult($"{nameof(ResultExpression)} is required", new[] { nameof(ResultExpression) });
        }

        if (DefaultExpression == null)
        {
            yield return new ValidationResult($"{nameof(DefaultExpression)} is required", new[] { nameof(DefaultExpression) });
        }
    }
}
