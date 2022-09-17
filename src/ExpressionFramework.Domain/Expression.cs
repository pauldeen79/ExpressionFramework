namespace ExpressionFramework.Domain;

public partial record Expression
{
    public abstract Result<object?> Evaluate(object? context);
}
