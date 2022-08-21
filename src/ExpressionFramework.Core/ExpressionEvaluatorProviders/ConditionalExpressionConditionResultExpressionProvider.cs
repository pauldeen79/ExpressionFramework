﻿namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class ConditionalExpressionConditionResultExpressionProvider : IExpressionEvaluatorProvider
{
    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        if (expression is not IConditionalExpressionConditionResultExpression)
        {
            result = default;
            return false;
        }

        result = default;
        return true;
    }
}
