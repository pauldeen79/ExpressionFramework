namespace ExpressionFramework.Domain;

public partial record Expression
{
    public abstract Result<object?> Evaluate(object? context);

    public virtual IEnumerable<ValidationResult> ValidateWithContext(object? context, ValidationContext validationContext)
        => Enumerable.Empty<ValidationResult>();
}
