namespace ExpressionFramework.Domain.Expressions;

public partial record DelegateExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value.Invoke(context));
}
