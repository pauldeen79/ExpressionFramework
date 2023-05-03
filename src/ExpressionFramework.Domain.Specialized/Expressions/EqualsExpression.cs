﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(EqualsExpression))]
public partial record EqualsExpression
{
    public override Result<object?> Evaluate(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? nonSuccessfulResult
            : Result<object?>.FromExistingResult(Equal.Evaluate(results[0], results[1], StringComparison.CurrentCultureIgnoreCase));
    }

    public Result<bool> EvaluateTyped(object? context)
    {
        var results = new[] { FirstExpression, SecondExpression }.EvaluateUntilFirstError(context);

        var nonSuccessfulResult = results.FirstOrDefault(x => !x.IsSuccessful());
        return nonSuccessfulResult != null
            ? Result<bool>.FromExistingResult(nonSuccessfulResult)
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
