﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the context to pascal case")]
[UsesContext(true)]
[ContextDescription("String to convert to pascal case")]
[ContextRequired(true)]
[ContextType(typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the context converted to pascal case", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Context must be of type string")]
public partial record ToPascalCaseExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => context is string s
            ? Result<object?>.Success(ToPascalCase(s))
            : Result<object?>.Invalid("Context must be of type string");

    public Result<string> EvaluateTyped(object? context)
        => context is string s
            ? Result<string>.Success(ToPascalCase(s))
            : Result<string>.Invalid("Context must be of type string");

    private string ToPascalCase(string value)
    {
        if (value.Length > 0)
        {
            return value.Substring(0, 1).ToLowerInvariant() + value.Substring(1);
        }

        return value;
    }
}
