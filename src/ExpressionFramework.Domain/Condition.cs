namespace ExpressionFramework.Domain;

public partial record Condition : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LeftExpression == null)
        {
            yield return new ValidationResult($"{nameof(LeftExpression)} is required", new[] { nameof(LeftExpression) });
        }

        if (RightExpression == null)
        {
            yield return new ValidationResult($"{nameof(RightExpression)} is required", new[] { nameof(RightExpression) });
        }
    }
}
