﻿namespace ExpressionFramework.Domain.Operators;

public partial record NotEndsWithOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue != null && !EndsWithOperator.IsValid(leftValue, rightValue));
}

