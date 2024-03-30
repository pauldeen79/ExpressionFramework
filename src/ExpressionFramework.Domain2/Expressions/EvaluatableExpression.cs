namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates the specified condition, and returns the result")]
[UsesContext(true)]
[ContextDescription("Context to use on condition evaluation")]
[ContextRequired(false)]
[ContextType(typeof(object))]
[ParameterDescription(nameof(Condition), "Condition to evaluate")]
[ParameterRequired(nameof(Condition), true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false", "The result of the condition evaluation")]
public partial record EvaluatableExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(Expression));

    public Result<bool> EvaluateTyped(object? context)
    {
        var expressionResult = Expression.Evaluate(context);
        return expressionResult.IsSuccessful()
            ? Condition.Evaluate(expressionResult.Value)
            : Result.FromExistingResult<bool>(expressionResult);
    }
}
