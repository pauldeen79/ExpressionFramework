namespace ExpressionFramework.Domain.Expressions;

public partial record EvaluatableExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(Condition.Evaluate(context), value => value);

    public Result<bool> EvaluateAsBoolean(object? context)
        => Condition.Evaluate(context);
}

