namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns false")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "false", "This result will always be returned")]
public partial record FalseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.Success<object?>(false);

    public Result<bool> EvaluateTyped(object? context)
        => Result.Success<bool>(false);
}
