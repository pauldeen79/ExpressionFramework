namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is null")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false, depending whether the left value is null", "This result will always be returned")]
public partial record IsNullOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result.Success(leftValue is null);
}
