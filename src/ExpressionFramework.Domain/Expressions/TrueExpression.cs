namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns true")]
[ExpressionUsesContext(false)]
[ReturnValue(ResultStatus.Ok, "true", "This result will always be returned")]
public partial record TrueExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(true);
}

