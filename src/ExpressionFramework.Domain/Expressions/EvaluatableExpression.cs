namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates the specified condition, and returns the result")]
[ParameterDescription(nameof(Condition), "Condition to evaluate")]
[ParameterRequired(nameof(Condition), true)]
[ReturnValue(ResultStatus.Ok, "true or false", "The result of the condition evaluation")]
public partial record EvaluatableExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Condition.Evaluate(context), value => value);

    public Result<bool> EvaluateAsBoolean(object? context)
        => Condition.Evaluate(context);
}

