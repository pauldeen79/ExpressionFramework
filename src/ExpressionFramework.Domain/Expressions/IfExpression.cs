﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates a condition")]
[UsesContext(true)]
[ContextDescription("Context that will be used as context on the condition")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Condition), "Condition to evaluate")]
[ParameterRequired(nameof(Condition), true)]
[ParameterDescription(nameof(ResultExpression), "Expression to use when the condition evaluates to true")]
[ParameterRequired(nameof(ResultExpression), true)]
[ParameterDescription(nameof(DefaultExpression), "Optional expression to use when the condition evaluates to false. When left empty, and empty expression will be used.")]
[ParameterRequired(nameof(DefaultExpression), false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Value of the ResultExpression, value of the DefaultExpression or empty value", "Value of the ResultExpression when condition evaluates to true, value of the DefaultExpression (when available and condition evaluates to false), or empty value (when DefaultExpression is not provided and condition evaluates to false")]
[ReturnValue(ResultStatus.Error, "Empty", "Any unsuccessful result from either the condition evaluation, result expression evaluation or default expression evaluation")]
public partial record IfExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = new EvaluatableExpression(Condition, ResultExpression).EvaluateTyped(context);
        if (!result.IsSuccessful())
        {
            return Result.FromExistingResult<object?>(result);
        }

        if (result.Value)
        {
            return ResultExpression.Evaluate(context);
        }

        if (DefaultExpression is not null)
        {
            return DefaultExpression.Evaluate(context);
        }

        return Result.Success<object?>(null);
    }
}
