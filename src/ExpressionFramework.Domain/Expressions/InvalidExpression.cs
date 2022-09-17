namespace ExpressionFramework.Domain.Expressions;

public partial record InvalidExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Invalid(ErrorMessage, ValidationErrors);
}

