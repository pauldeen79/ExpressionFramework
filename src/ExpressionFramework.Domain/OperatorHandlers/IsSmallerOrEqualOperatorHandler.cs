﻿namespace ExpressionFramework.Domain.OperatorHandlers;

public class IsSmallerOrEqualOperatorHandler : OperatorHandlerBase<IsSmallerOrEqualOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => leftValue != null
        && rightValue != null
        && leftValue is IComparable c
        && c.CompareTo(rightValue) <= 0;
}
