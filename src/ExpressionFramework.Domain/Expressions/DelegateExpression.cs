namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a value from a delegate")]
[ParameterDescription(nameof(Value), "Delegate to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, "The return value from the delegate that is supplied with the Value parameter", "This result will always be returned")]
public partial record DelegateExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value.Invoke(context));
}
