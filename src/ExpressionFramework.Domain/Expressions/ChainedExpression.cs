namespace ExpressionFramework.Domain.Expressions;

public partial record ChainedExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        if (!Expressions.Any())
        {
            return Result<object?>.Invalid("No expressions found");
        }

        return Expressions.Aggregate(Result<object?>.Success(context), (seed, accumulator) =>
        {
            if (!seed.IsSuccessful())
            {
                return seed;
            }
            return accumulator.Evaluate(seed.Value);
        });
    }
}
