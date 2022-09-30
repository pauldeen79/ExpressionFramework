namespace ExpressionFramework.Domain.Expressions;

public partial record OperatorExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateAsBoolean(context), value => value);

    public Result<bool> EvaluateAsBoolean(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}

