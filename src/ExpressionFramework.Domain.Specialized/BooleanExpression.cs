namespace ExpressionFramework.Domain;

public static class BooleanExpression
{
    public static Result<bool> EvaluateBooleanCombination(
        object? context,
        ITypedExpression<bool> firstExpression,
        ITypedExpression<bool> secondExpression,
        Func<bool, bool, bool> @delegate)
    {
        if (firstExpression is null)
        {
            return Result.Invalid<bool>("First expression is required");
        }

        if (secondExpression is null)
        {
            return Result.Invalid<bool>("Second expression is required");
        }

        if (@delegate is null)
        {
            return Result.Invalid<bool>("Delegate is required");
        }

        var firstExpressionResult = firstExpression.EvaluateTyped(context);
        if (!firstExpressionResult.IsSuccessful())
        {
            return Result.FromExistingResult<bool>(firstExpressionResult);
        }

        var secondExpressionResult = secondExpression.EvaluateTyped(context);
        if (!secondExpressionResult.IsSuccessful())
        {
            return Result.FromExistingResult<bool>(secondExpressionResult);
        }

        return Result.Success(@delegate.Invoke(firstExpressionResult.Value, secondExpressionResult.Value));
    }

    public static ExpressionDescriptor GetDescriptor(Type type,
                                                     string description,
                                                     string okValue,
                                                     string okDescription,
                                                     string? invalidDescription,
                                                     string parameterDescription)
    {
        type = ArgumentGuard.IsNotNull(type, nameof(type));

        return new(
            type.Name,
            type.FullName,
            description,
            true,
            null,
            "Context to use on expression evaluation",
            null,
            new[]
            {
                new ParameterDescriptor("FirstExpression", typeof(bool).FullName, parameterDescription, true),
                new ParameterDescriptor("SecondExpression", typeof(bool).FullName, parameterDescription, true),
            },
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(object), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", typeof(object), invalidDescription!),
            }.Where(x => invalidDescription != null || x.Status != ResultStatus.Invalid)
        );
    }
}
