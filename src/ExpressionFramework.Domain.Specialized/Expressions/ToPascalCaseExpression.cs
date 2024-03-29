﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Converts the expression to pascal case")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(Expression), "String to get the pascal case for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The value of the expression converted to pascal case", "This result will be returned when the expression is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string")]
public partial record ToPascalCaseExpression
{
    public override Result<object?> Evaluate(object? context)
        => Result.FromExistingResult<object?>(EvaluateTyped(context));

    public Result<string> EvaluateTyped(object? context)
        => Expression.EvaluateTypedWithTypeCheck(context).Transform(result =>
            result.IsSuccessful()
                ? Result.Success(ToPascalCase(result.Value!))
                : result);

    private string ToPascalCase(string value)
        => value.Length > 0
            ? value.Substring(0, 1).ToLowerInvariant() + value.Substring(1)
            : value;
}
