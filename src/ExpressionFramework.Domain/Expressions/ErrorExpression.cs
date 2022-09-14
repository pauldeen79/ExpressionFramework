namespace ExpressionFramework.Domain.Expressions;

public partial record ErrorExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Error(ErrorMessage);
}
