﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("This expression always returns an empty value")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, "Empty", "This result will always be returned")]
public partial record EmptyExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(null);
}

public partial record EmptyExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
