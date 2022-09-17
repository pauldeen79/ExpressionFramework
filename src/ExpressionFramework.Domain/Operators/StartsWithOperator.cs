﻿namespace ExpressionFramework.Domain.Operators;

public partial record StartsWithOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(IsValid(leftValue, rightValue));

    internal static bool IsValid(object? leftValue, object? rightValue)
    {
        if (leftValue == null)
        {
            return false;
        }

        if (rightValue == null)
        {
            return false;
        }

        var rightValueString = rightValue.ToString();
        if (string.IsNullOrEmpty(rightValueString))
        {
            return false;
        }

        return leftValue.ToString().StartsWith(rightValueString, StringComparison.CurrentCultureIgnoreCase);
    }
}

