﻿namespace ExpressionFramework.Domain.Expressions;

[ExpressionDescription("Evaluates two expressions, and compares the two results. It will return true when they are equal, or false otherwise.")]
[ParameterDescription(nameof(FirstExpression), "First expression")]
[ParameterRequired(nameof(FirstExpression), true)]
[ParameterDescription(nameof(SecondExpression), "Second expression")]
[ParameterRequired(nameof(SecondExpression), true)]
[ReturnValue(ResultStatus.Ok, "true of false", "This result will always be returned")]
public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result<object?>.Success(EqualsOperator.IsValid(results[0], results[1]));
    }
}
