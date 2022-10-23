namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value starts with the right value")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(string))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false", "True when both values are string, and the left value starts with the right value, otherwise false")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Left value or right value are not of type string")]
public partial record StartsWithOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => leftValue is string leftString && rightValue is string rightString
            ? Result<bool>.Success(leftString.StartsWith(rightString, StringComparison.CurrentCultureIgnoreCase))
            : Result<bool>.Invalid("LeftValue and RightValue both need to be of type string");
}

