﻿namespace ExpressionFramework.Core.ExpressionEvaluatorHandlers;

public class ConditionalExpressionEvaluatorHandler : IExpressionEvaluatorHandler
{
    private readonly IConditionEvaluatorProvider _conditionEvaluatorProvider;

    public ConditionalExpressionEvaluatorHandler(IConditionEvaluatorProvider conditionEvaluatorProvider)
        => _conditionEvaluatorProvider = conditionEvaluatorProvider;

    public Result<object?> Handle(object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        if (expression is not IConditionalExpression conditionalExpression)
        {
            return Result<object?>.NotSupported();
        }

        var conditionResult = _conditionEvaluatorProvider.Get(evaluator).Evaluate(context, conditionalExpression.Conditions);
        if (!conditionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(conditionResult);
        }

        return evaluator.Evaluate
        (
            context,
            conditionResult.Value
                ? conditionalExpression.ResultExpression
                : conditionalExpression.DefaultExpression
        );
    }
}
