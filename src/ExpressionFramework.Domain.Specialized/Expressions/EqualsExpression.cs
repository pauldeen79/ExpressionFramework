﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(EqualsExpression))]
public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = Array.Find(results, x => !x.IsSuccessful());
        return nonSuccessfulResult is not null
            ? nonSuccessfulResult
            : Result.FromExistingResult<object?>(Equal.Evaluate(results[0], results[1], StringComparison.CurrentCultureIgnoreCase));
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = Array.Find(results, x => !x.IsSuccessful());
        return nonSuccessfulResult is not null
            ? Result.FromExistingResult<bool>(nonSuccessfulResult)
            : Equal.Evaluate(results[0], results[1], StringComparison.CurrentCultureIgnoreCase);
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
