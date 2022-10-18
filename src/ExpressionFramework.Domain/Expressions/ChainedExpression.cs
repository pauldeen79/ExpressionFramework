namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Chains the result of an expression onto the next one, and so on")]
[UsesContext(true)]
[ContextDescription("Value to use as seed (initiation) for the first expression")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Expressions), "Expressions to use on chaining. The context is chained to the first expression.")]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the last expression returns success (Ok)")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case any expression returns something else than Ok, in which case subsequent expressions will not be executed anymore")]
public partial record ChainedExpression
{
    public override Result<object?> Evaluate(object? context)
        => Expressions.Aggregate(Result<object?>.Success(context), (seed, accumulator)
            => !seed.IsSuccessful()
                ? seed
                : accumulator.Evaluate(seed.Value));
}
