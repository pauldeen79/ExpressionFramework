﻿namespace ExpressionFramework.Domain;

public static class BooleanExpression
{
    public static Result<bool> EvaluateBooleanCombination(
        object? context,
        Expression expression,
        Func<bool, bool, bool> @delegate)
    {
        if (context is not bool b)
        {
            return Result<bool>.Invalid("Context must be of type boolean");
        }

        var expressionResult = expression.EvaluateTyped<bool>(context, "Expression must be of type boolean");
        if (!expressionResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(expressionResult);
        }

        return Result<bool>.Success(@delegate.Invoke(b, expressionResult.Value));
    }
}