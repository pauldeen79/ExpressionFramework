﻿namespace ExpressionFramework.Domain.Operators;

public partial record IsNullOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => Result<bool>.Success(leftValue == null);
}