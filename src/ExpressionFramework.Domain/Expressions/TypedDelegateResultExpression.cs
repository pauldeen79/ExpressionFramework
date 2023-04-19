namespace ExpressionFramework.Domain.Expressions;

public partial record TypedDelegateResultExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<T> EvaluateTyped(object? context)
        => Value.Invoke(context);

    public Expression ToUntyped() => new DelegateResultExpression(ctx => Result<object?>.FromExistingResult(Value.Invoke(ctx)));
}

public partial record TypedDelegateResultExpressionBase<T>
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
