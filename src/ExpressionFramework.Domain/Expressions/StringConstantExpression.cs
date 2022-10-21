﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a constant value")]
[UsesContext(false)]
[ParameterDescription(nameof(Value), "Value to use")]
[ParameterRequired(nameof(Value), true)]
[ReturnValue(ResultStatus.Ok, typeof(object), "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record StringConstantExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value);

    public Result<string> EvaluateTyped(object? context)
        => Result<string>.Success(Value);
}

