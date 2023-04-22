namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a typed result value from a typed delegate")]
[UsesContext(false)]
[ParameterDescription(nameof(Value), "Delegate to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "The return value from the delegate that is supplied with the Value parameter", "This result will always be returned")]
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
