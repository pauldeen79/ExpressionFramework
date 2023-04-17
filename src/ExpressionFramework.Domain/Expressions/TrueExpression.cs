namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns true")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true", "This result will always be returned")]
public partial record TrueExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(true);

    public Result<bool> EvaluateTyped(object? context)
        => Result<bool>.Success(true);

    public Expression ToUntyped() => this;
}

public partial record TrueExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
