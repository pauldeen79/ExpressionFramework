namespace ExpressionFramework.Domain.Expressions;

public partial record ContextExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(context);
}
