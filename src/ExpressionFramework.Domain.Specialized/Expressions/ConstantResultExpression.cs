namespace ExpressionFramework.Domain.Expressions;

public partial record ConstantResultExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(Value);
}

