namespace ExpressionFramework.Domain.Expressions;

public partial record ChainedExpression
{
    public override Result<object?> Evaluate(object? context)
        => Expressions.Aggregate(Result<object?>.Success(context), (seed, accumulator)
            => !seed.IsSuccessful()
            ? seed
            : accumulator.Evaluate(seed.Value));
}
