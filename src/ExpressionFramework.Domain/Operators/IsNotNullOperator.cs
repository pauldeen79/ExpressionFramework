namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value is not null")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(false)]
[ReturnValue(ResultStatus.Ok, "true or false, depending whether the left value is not null", "This result will always be returned")]
public partial record IsNotNullOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null);
}
