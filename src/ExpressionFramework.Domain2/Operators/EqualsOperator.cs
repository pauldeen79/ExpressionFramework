namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left and right value are equal")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false, depending whether the values are equal", "This result will always be returned")]
public partial record EqualsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Equal.Evaluate(leftValue, rightValue, StringComparison.CurrentCultureIgnoreCase);
}
