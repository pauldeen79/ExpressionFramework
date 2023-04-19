namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns false")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "false", "This result will always be returned")]
public partial record FalseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(false);

    public Result<bool> EvaluateTyped(object? context)
        => Result<bool>.Success(false);

    public Expression ToUntyped() => this;
}

public partial record FalseExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
