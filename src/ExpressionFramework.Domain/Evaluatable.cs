namespace ExpressionFramework.Domain;

public partial record Evaluatable
{
    public Result<bool> Evaluate() => Evaluate(null);

    public abstract Result<bool> Evaluate(object? context);
}
