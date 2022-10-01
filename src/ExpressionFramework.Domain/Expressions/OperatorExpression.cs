namespace ExpressionFramework.Domain.Expressions;

public partial record OperatorExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<bool> EvaluateTyped(object? context)
        => Operator.Evaluate(context, LeftExpression, RightExpression);
}

