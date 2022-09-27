namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a constant value")]
[ExpressionContextDescription("Value to use")]
[ExpressionContextType(typeof(object))]
[ExpressionContextRequired(true)]
[ReturnValue(ResultStatus.Ok, "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record ConstantExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value);
}
