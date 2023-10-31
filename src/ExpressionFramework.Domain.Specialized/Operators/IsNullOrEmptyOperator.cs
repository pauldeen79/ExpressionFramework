namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is null or an empty string")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false, depending whether the left value is null or an empty string", "This result will always be returned")]
public partial record IsNullOrEmptyOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result.Success<bool>(leftValue is null || leftValue.ToString().Length == 0);
}
