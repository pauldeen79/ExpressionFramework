namespace ExpressionFramework.Domain.Expressions;

public partial record ChainedExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        Result<object?>? result = null;
        var first = true;
        foreach (var exp in Expressions)
        {
            if (first)
            {
                result = exp.Evaluate(context);
                first = false;
            }
            else
            {
                result = exp.Evaluate(result!.Value);
            }
            if (!result.IsSuccessful())
            {
                return result;
            }
        }

        return result ?? Result<object?>.Invalid("No expressions found");
    }
}
