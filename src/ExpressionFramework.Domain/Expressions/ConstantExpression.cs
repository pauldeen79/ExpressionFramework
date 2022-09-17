namespace ExpressionFramework.Domain.Expressions;

public partial record ConstantExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value);
}
