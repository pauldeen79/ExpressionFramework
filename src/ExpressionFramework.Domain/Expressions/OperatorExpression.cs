namespace ExpressionFramework.Domain.Expressions;

public partial record OperatorExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var result = Operator.Evaluate(context, LeftExpression, RightExpression);
        if (!result.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result);
        }

        return Result<object?>.Success(result.Value);
    }

    public Result<bool> EvaluateAsBoolean(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}

