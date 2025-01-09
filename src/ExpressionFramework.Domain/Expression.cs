namespace ExpressionFramework.Domain;

public partial record Expression : IExpression
{
    public Result<object?> Evaluate() => Evaluate(null);

    public abstract Result<object?> Evaluate(object? context);
}
