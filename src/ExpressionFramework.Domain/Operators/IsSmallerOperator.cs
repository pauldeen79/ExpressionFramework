namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is smaller than the right value")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true when the left value is smaller than the right value, otherwise false", "This result will always be returned")]
public partial record IsSmallerOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => SmallerThan.Evaluate(leftValue, rightValue);
}
