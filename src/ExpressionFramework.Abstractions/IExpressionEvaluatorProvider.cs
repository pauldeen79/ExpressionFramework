﻿namespace ExpressionFramework.Abstractions;

public interface IExpressionEvaluatorProvider
{
    bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result);
}
