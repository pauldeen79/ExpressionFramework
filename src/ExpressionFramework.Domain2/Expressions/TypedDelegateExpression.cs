namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a value from a typed delegate")]
[UsesContext(false)]
[ParameterDescription(nameof(Value), "Delegate to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "The return value from the delegate that is supplied with the Value parameter", "This result will always be returned")]
public partial record TypedDelegateExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result.Success<object?>(Value.Invoke(context));

    public Result<T> EvaluateTyped(object? context)
        => Result.Success(Value.Invoke(context));

    public Expression ToUntyped()
        => new DelegateExpression(context => Value.Invoke(context));
}
