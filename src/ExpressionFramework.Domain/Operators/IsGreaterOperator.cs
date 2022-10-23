namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is greater than the right value")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true when the left value is greater than the right value, otherwise false", "This result will always be returned")]
public partial record IsGreaterOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        =>  Result<bool>.Success(leftValue != null
        && rightValue != null
        && leftValue is IComparable c
        && c.CompareTo(rightValue) > 0);
}
