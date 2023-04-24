namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is null or a whitespace-only string")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false, depending whether the left value is null or a whitespace-only string", "This result will always be returned")]
public partial record IsNullOrWhiteSpaceOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue is null || leftValue.ToString().Trim() == string.Empty);
}
