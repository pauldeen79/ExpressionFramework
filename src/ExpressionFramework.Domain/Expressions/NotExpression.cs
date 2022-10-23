﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns the inverted value of the boolean context value")]
[UsesContext(true)]
[ContextDescription("Boolean value to invert")]
[ContextType(typeof(bool))]
[ContextRequired(true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "Inverted value of the boolean context value", "This result will be returned when the context is a boolean value")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type boolean")]
public partial record NotExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
        => context is bool b
            ? Result<object?>.Success(!b)
            : Result<object?>.Invalid("Context must be of type boolean");

    public Result<bool> EvaluateTyped(object? context)
        => context is bool b
            ? Result<bool>.Success(!b)
            : Result<bool>.Invalid("Context must be of type boolean");
}

