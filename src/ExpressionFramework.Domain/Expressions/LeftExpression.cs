﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Gets a number of characters of the start of a string value of the context")]
[UsesContext(true)]
[ContextDescription("Context to use on expression evaluation")]
[ParameterDescription(nameof(LengthExpression), "Number of characters to use")]
[ParameterRequired(nameof(LengthExpression), true)]
[ParameterType(nameof(LengthExpression), typeof(int))]
[ParameterDescription(nameof(Expression), "String to get the first characters for")]
[ParameterRequired(nameof(Expression), true)]
[ParameterType(nameof(Expression), typeof(string))]
[ReturnValue(ResultStatus.Ok, typeof(string), "The first characters of the context", "This result will be returned when the context is of type string")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Expression must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string")]
public partial record LeftExpression : ITypedExpression<string>
{
    public override Result<object?> Evaluate(object? context)
        => Result<object?>.FromExistingResult(EvaluateTyped(context), value => value);

    public Result<string> EvaluateTyped(object? context)
    {
        var stringResult = Expression.EvaluateTyped<string>(context, "Expression must be of type string");
        if (!stringResult.IsSuccessful())
        {
            return stringResult;
        }

        return GetLeftValueFromString(stringResult.Value!);
    }

    private Result<string> GetLeftValueFromString(string s)
    {
        var lengthResult = LengthExpression.Evaluate(s);
        if (!lengthResult.IsSuccessful())
        {
            return Result<string>.FromExistingResult(lengthResult);
        }

        if (lengthResult.Value is not int length)
        {
            return Result<string>.Invalid("LengthExpression did not return an integer");
        }

        return s.Length >= length
            ? Result<string>.Success(s.Substring(0, length))
            : Result<string>.Invalid("Length must refer to a location within the string");
    }
}

