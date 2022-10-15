﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Returns a constant value")]
[ExpressionUsesContext(false)]
[ParameterDescription(nameof(Value), "Value to use")]
[ParameterRequired(nameof(Value), true)]
[ParameterType(nameof(Value), typeof(object))]
[ReturnValue(ResultStatus.Ok, typeof(object), "The value that is supplied with the Value parameter", "This result will always be returned")]
public partial record ConstantExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.Success(Value);
}
