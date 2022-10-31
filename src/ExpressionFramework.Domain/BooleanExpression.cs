﻿namespace ExpressionFramework.Domain;

public static class BooleanExpression
{
    public static Result<bool> EvaluateBooleanCombination(
        object? context,
        Expression firstExpression,
        Expression secondExpression,
        Func<bool, bool, bool> @delegate)
    {
        var firstExpressionResult = firstExpression.EvaluateTyped<bool>(context, "FirstExpression must be of type boolean");
        if (!firstExpressionResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(firstExpressionResult);
        }

        var secondExpressionResult = secondExpression.EvaluateTyped<bool>(context, "SecondExpression must be of type boolean");
        if (!secondExpressionResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(secondExpressionResult);
        }

        return Result<bool>.Success(@delegate.Invoke(firstExpressionResult.Value, secondExpressionResult.Value));
    }

    public static ExpressionDescriptor GetDescriptor(Type type,
                                                     string description,
                                                     string okValue,
                                                     string okDescription,
                                                     string invalidDescription,
                                                     string parameterDescription)
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            typeof(IEnumerable).FullName,
            "Boolean value to use",
            true,
            new[]
            {
                new ParameterDescriptor("FirstExpression", typeof(Expression).FullName, parameterDescription, true),
                new ParameterDescriptor("SecondExpression", typeof(Expression).FullName, parameterDescription, true),
            },
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(object), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", typeof(object), invalidDescription),
            });
}
