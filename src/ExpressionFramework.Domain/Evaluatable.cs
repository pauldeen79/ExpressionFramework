namespace ExpressionFramework.Domain;

public partial record Evaluatable
{
    public abstract Result<bool> Evaluate(object? context);
}
