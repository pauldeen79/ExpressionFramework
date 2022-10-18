namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates the specified condition, and returns the result")]
[UsesContext(true)]
[ContextDescription("Context to use on condition evaluation")]
[ContextRequired(false)]
[ContextType(typeof(object))]
[ParameterDescription(nameof(Condition), "Condition to evaluate")]
[ParameterRequired(nameof(Condition), true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true or false", "The result of the condition evaluation")]
public partial record EvaluatableExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<bool> EvaluateTyped(object? context)
        => Condition.Evaluate(context);
}

