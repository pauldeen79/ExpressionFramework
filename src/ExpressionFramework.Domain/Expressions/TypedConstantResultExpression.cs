namespace ExpressionFramework.Domain.Expressions;

public partial record TypedConstantResultExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Value, value => value);

    public Result<T> EvaluateTyped(object? context)
        => Value;

    public Expression ToUntyped()
        => new ConstantExpression(Value.Value);
}

public partial record TypedConstantResultExpressionBase<T>
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
