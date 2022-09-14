namespace ExpressionFramework.Domain.Expressions;

public partial record EmptyExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(null);
}
