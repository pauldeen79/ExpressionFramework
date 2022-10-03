namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns false")]
[ExpressionUsesContext(false)]
[ReturnValue(ResultStatus.Ok, "false", "This result will always be returned")]
public partial record FalseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(false);
}

