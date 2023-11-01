namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Chains the result of an expression onto the next one, and so on")]
[UsesContext(true)]
[ContextDescription("Value to use as context in expression evaluation")]
[ContextType(typeof(object))]
[ContextRequired(false)]
[ParameterDescription(nameof(Expressions), "Expressions to use on chaining. The context is chained to the first expression.")]
[ParameterRequired(nameof(Expressions), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Result value of the last expression", "This will be returned in case the last expression returns success (Ok)")]
[ReturnValue(ResultStatus.Error, "Empty", "This status (or any other status not equal to Ok) will be returned in case any expression returns something else than Ok, in which case subsequent expressions will not be executed anymore")]
public partial record TypedChainedExpression<T>
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Expression ToUntyped() => new ChainedExpression(Expressions);

    public Result<T> EvaluateTyped(object? context)
    {
        if (!Expressions.Any())
        {
            return Result.Success(context is T t ? t : default!);
        }

        var result = Expressions.First().Evaluate(context);
        if (!result.IsSuccessful())
        {
            return Result.FromExistingResult<T>(result);
        }

        foreach (var expression in Expressions.Skip(1))
        {
            result = expression.Evaluate(result.Value);
            if (!result.IsSuccessful())
            {
                return Result.FromExistingResult<T>(result);
            }
        }

        return result.TryCast<T>(ExpressionExtensions.CreateInvalidTypeErrorMessage<T>());
    }
}
