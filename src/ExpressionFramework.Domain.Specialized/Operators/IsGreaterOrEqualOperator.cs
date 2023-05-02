namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is greater or equal than the right value")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true when the left value is greater or equal than the right value, otherwise false", "This result will always be returned")]
public partial record IsGreaterOrEqualOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => GreaterOrEqualThan.Evaluate(leftValue, rightValue);
}
