﻿namespace ExpressionFramework.Domain.Aggregators;

[AggregatorDescription("Concatenates two string values")]
[UsesContext(false)]
[ReturnValue(ResultStatus.Ok, typeof(object), "Concatenation of two string values", "This will be returned in case the execution returns success (Ok)")]
[ReturnValue(ResultStatus.Invalid, "Empty", "First expressio is not of type string, Second expression is not of type string")]
public partial record StringConcatenateAggregator
{
    public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression)
    {
        var result1 = firstExpression.EvaluateTyped<string>(context, "First expression is not of type string");
        if (!result1.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result1);
        }

        var result2 = secondExpression.EvaluateTyped<string>(context, "Second expression is not of type string");
        if (!result2.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(result2);
        }

        return Result<object?>.Success(result1.Value + result2.Value);
    }
}

