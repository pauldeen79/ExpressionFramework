﻿namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class SwitchEpressionEvaluationHandler : IExpressionEvaluatorHandler
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public SwitchEpressionEvaluationHandler(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not ISwitchExpression switchExpression)
        {
            return Result<object?>.NotSupported();
        }

        var conditionEvaluator = _conditionEvaluatorProvider.Get(evaluator);
        foreach (var @case in switchExpression.Cases)
        {
            var caseResult = conditionEvaluator.Evaluate(context, @case.Conditions);
            if (!caseResult.IsSuccessful())
            {
                return Result<object?>.FromExistingResult(caseResult);
            }
            if (caseResult.Value)
            {
                return evaluator.Evaluate(context, @case.Expression);
            }
        }

        return evaluator.Evaluate(context, switchExpression.DefaultExpression);
    }
}