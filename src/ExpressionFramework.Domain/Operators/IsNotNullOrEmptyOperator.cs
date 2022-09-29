namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is null or an empty string")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(false)]
[ReturnValue(ResultStatus.Ok, "true or false, depending whether the left value is null or an empty string", "This result will always be returned")]
public partial record IsNotNullOrEmptyOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(!(leftValue == null || leftValue.ToString() == string.Empty));
}
