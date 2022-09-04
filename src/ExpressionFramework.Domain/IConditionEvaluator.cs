﻿namespace ExpressionFramework.Domain;

public interface IConditionEvaluator
{
    Result<bool> Evaluate(object? context, IEnumerable<Condition> conditions);
}