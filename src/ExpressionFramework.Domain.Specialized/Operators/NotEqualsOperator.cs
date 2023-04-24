namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left and right value are unequal")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false, depending whether the values are unequal", "This result will always be returned")]
public partial record NotEqualsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(!EqualsOperator.IsValid(leftValue, rightValue));
}

