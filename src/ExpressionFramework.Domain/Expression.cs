namespace ExpressionFramework.Domain;

public partial record Expression
{
    public abstract Result<object?> Evaluate(object? context);

    public IEnumerable<ValidationResult> ValidateContext(object? context)
        => ValidateContext(context, new ValidationContext(this));

    public virtual IEnumerable<ValidationResult> ValidateContext(object? context, ValidationContext validationContext)
        => Enumerable.Empty<ValidationResult>();
}
