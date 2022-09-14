namespace ExpressionFramework.Domain.Expressions;

public partial record ToLowerCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(context?.ToString().ToLower());
}
