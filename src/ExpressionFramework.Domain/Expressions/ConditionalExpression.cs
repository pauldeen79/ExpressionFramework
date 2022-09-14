namespace ExpressionFramework.Domain.Expressions;

public partial record ConditionalExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = new TernaryExpression(Conditions).EvaluateAsBoolean(context);
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
}
