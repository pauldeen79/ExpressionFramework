namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a typed constant value")]
[UsesContext(false)]
[ParameterDescription(nameof(Value), "Value to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record TypedConstantExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value);

    public Result<T> EvaluateTyped(object? context)
        => Result<T>.Success(Value);

    public Expression ToUntyped()
        => new ConstantExpression(Value);
}
