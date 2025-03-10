﻿namespace ExpressionFramework.Domain;

public static class StringExpression
{
    public static ExpressionDescriptor GetStringEdgeDescriptor(
        Type type,
        string description,
        string expressionDescription,
        string okValue,
        string okDescription)
        => GetDescriptor(
            ArgumentGuard.IsNotNull(type, nameof(type)),
            description,
            expressionDescription,
            okValue,
            okDescription,
            "LengthExpression",
            "Number of characters to use",
            typeof(int),
            true,
            "Length must refer to a location within the string");

    public static ExpressionDescriptor GetStringTrimDescriptor(
        Type type,
        string description,
        string expressionDescription,
        string okValue,
        string okDescription)
        => GetDescriptor(
            ArgumentGuard.IsNotNull(type, nameof(type)),
            description,
            expressionDescription,
            okValue,
            okDescription,
            "TrimCharsExpression",
            "Optional trim characters to use. When empty, space will be used",
            typeof(char[]),
            false,
            "Expression must be of type string");

    public static Result<string> EvaluateCultureExpression(
        ITypedExpression<string> expression,
        ITypedExpression<CultureInfo>? cultureExpression,
        object? context,
        Func<CultureInfo, string, string> cultureDelegate,
        Func<string, string> noCultureDelegate)
    {
        if (cultureExpression is null)
        {
            return expression.EvaluateTypedWithTypeCheck(context).Either(
                error => error,
                success => Result.Success(noCultureDelegate(success.Value!))
            );
        }

        var cultureResult = cultureExpression.EvaluateTyped();
        if (!cultureResult.IsSuccessful())
        {
            return Result.FromExistingResult<string>(cultureResult);
        }

        return expression.EvaluateTypedWithTypeCheck(context).Either(
                error => error,
                success => Result.Success(cultureDelegate(cultureResult.Value!, success.Value!))
            );
    }

#pragma warning disable S107 // Methods should not have too many parameters
    private static ExpressionDescriptor GetDescriptor(
        Type type,
        string description,
        string expressionDescription,
        string okValue,
        string okDescription,
        string customExpressionName,
        string customExpressionDescription,
        Type customExpressionType,
        bool customExpressionRequired,
        string invalidDescription)
#pragma warning restore S107 // Methods should not have too many parameters
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            null,
            null,
            null,
            [
                new ParameterDescriptor(customExpressionName, customExpressionType.FullName, customExpressionDescription, customExpressionRequired),
                new ParameterDescriptor("Expression", typeof(string).FullName, expressionDescription, true),
            ],
            [
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(string), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", null, invalidDescription),
            ]
        );
}
