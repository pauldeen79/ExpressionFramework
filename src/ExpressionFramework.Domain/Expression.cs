namespace ExpressionFramework.Domain;

public partial record Expression
{
    public Result<object?> Evaluate() => Evaluate(null);

    public abstract Result<object?> Evaluate(object? context);

    public virtual Result<Expression> GetPrimaryExpression() => Result<Expression>.NotSupported();
}
