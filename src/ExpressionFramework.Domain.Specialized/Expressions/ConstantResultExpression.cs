namespace ExpressionFramework.Domain.Expressions;

public partial record ConstantResultExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Value);
}

