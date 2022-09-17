namespace ExpressionFramework.Domain.Expressions;

public partial record ToUpperCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(context?.ToString().ToUpper());
}
