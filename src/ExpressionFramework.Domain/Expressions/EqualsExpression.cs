﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(EqualsExpression))]
public partial record EqualsExpression : ITypedExpression<bool>
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result<object?>.Success(EqualsOperator.IsValid(results[0], results[1]));
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result<bool>.FromExistingResult(nonSuccessfulResult)
            : Result<bool>.Success(EqualsOperator.IsValid(results[0], results[1]));
    }

    public static ExpressionDescriptor GetExpressionDescriptor()
        => BooleanExpression.GetDescriptor(
            typeof(EqualsExpression),
            "Evaluates two expressions, and compares the two results. It will return true when they are equal, or false otherwise.",
            "true of false",
            "This result will always be returned",
            null,
            "Boolean expression to perform Equals operation on");
}

public partial record EqualsExpressionBase
{
    public override Result<object?> Evaluate(object? context) => throw new NotImplementedException();
}
