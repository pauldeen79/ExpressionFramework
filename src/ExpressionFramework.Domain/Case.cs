namespace ExpressionFramework.Domain;

public partial record Case : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Conditions == null)
        {
            yield return new ValidationResult($"{nameof(Conditions)} is required", new[] { nameof(Conditions) });
        }

        if (Expression == null)
        {
            yield return new ValidationResult($"{nameof(Expression)} is required", new[] { nameof(Expression) });
        }
    }
}
