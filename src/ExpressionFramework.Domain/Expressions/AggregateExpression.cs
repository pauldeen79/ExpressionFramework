namespace ExpressionFramework.Domain.Expressions;

public partial record AggregateExpression : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Expressions == null)
        {
            yield return new ValidationResult($"{nameof(Expressions)} is required", new[] { nameof(Expressions) });
        }

        if (ExpressionConditions == null)
        {
            yield return new ValidationResult($"{nameof(ExpressionConditions)} is required", new[] { nameof(ExpressionConditions) });
        }

        if (Accumulator == null)
        {
            yield return new ValidationResult($"{nameof(Accumulator)} is required", new[] { nameof(Accumulator) });
        }
    }
}
