﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns true")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true", "This result will always be returned")]
public partial record TrueExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.Success<object?>(true);

    public Result<bool> EvaluateTyped(object? context)
        => Result.Success(true);
}
