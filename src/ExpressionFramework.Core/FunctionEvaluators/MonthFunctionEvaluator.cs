﻿namespace ExpressionFramework.Core.FunctionEvaluators;

public class MonthFunctionEvaluator : IFunctionEvaluator
{
    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        result = null;
        if (!(function is MonthFunction))
        {
            return false;
        }

        if (value is DateTime dateTime)
        {
            result = dateTime.Month;
        }

        return true;
    }
}
