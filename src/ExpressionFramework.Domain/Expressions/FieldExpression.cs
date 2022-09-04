namespace ExpressionFramework.Domain.Expressions;

public partial record FieldExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FieldName == null)
        {
            yield return new ValidationResult($"{nameof(FieldName)} is required", new[] { nameof(FieldName) });
        }
    }
}
