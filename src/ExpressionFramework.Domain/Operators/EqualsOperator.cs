﻿namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left and right value are equal")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(object))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, "true or false, depending whether the values are equal", "This result will always be returned")]
public partial record EqualsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(IsValid(leftValue, rightValue));

    internal static bool IsValid(object? leftValue, object? rightValue)
    {
        if (leftValue == null && rightValue == null)
        {
            return true;
        }

        if (leftValue == null || rightValue == null)
        {
            return false;
        }

        if (leftValue is string leftString
            && rightValue is string rightString
            && leftString.Equals(rightString, StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return leftValue.Equals(rightValue);
    }
}
