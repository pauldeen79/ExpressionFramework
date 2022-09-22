namespace ExpressionFramework.Domain.Expressions;

public partial record ConditionalExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = new EvaluatableExpression(Condition).EvaluateAsBoolean(context);
        if (!result.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result);
        }
        
        if (result.Value)
        {
            return ResultExpression.Evaluate(context);
        }

        if (DefaultExpression != null)
        {
            return DefaultExpression.Evaluate(context);
        }

        return Result<object?>.Success(null);
    }

    public Result<(bool ConditionResult, Result<object?> ExpressionResult)> EvaluateWithConditionResult(object? context)
    {
        var result = new EvaluatableExpression(Condition).EvaluateAsBoolean(context);
        if (!result.IsSuccessful())
        {
            return Result<(bool ConditionResult, Result<object?> ExpressionResult)>.FromExistingResult(result);
        }

        if (result.Value)
        {
            return Result<(bool ConditionResult, Result<object?> ExpressionResult)>.Success((true, ResultExpression.Evaluate(context)));
        }

        if (DefaultExpression != null)
        {
            return Result<(bool ConditionResult, Result<object?> ExpressionResult)>.Success((false, DefaultExpression.Evaluate(context)));
        }

        return Result<(bool ConditionResult, Result<object?> ExpressionResult)>.Success((false, Result<object?>.Success(null)));
    }
}
