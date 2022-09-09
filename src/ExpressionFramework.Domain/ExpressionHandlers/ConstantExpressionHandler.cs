﻿namespace ExpressionFramework.Domain.ExpressionHandlers;

public class ConstantExpressionHandler : ExpressionHandlerBase<ConstantExpression>
{
    protected override Task<Result<object?>> Handle(object? context, ConstantExpression typedExpression, IExpressionEvaluator evaluator)
        => Task.FromResult(Result<object?>.Success(typedExpression.Value));
}
