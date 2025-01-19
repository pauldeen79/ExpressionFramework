namespace ExpressionFramework.Parser.Extensions;

public static class FunctionCallContextExtensions
{
    public static ITypedExpression<T> GetArgumentValueExpression<T>(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<T>(argumentName, context.GetArgumentValueResult(index, argumentName));

    public static ITypedExpression<T> GetArgumentValueExpression<T>(this FunctionCallContext context, int index, string argumentName, T? defaultValue)
        => ProcessArgumentResult(argumentName, context.GetArgumentValueResult(index, argumentName, defaultValue), true, defaultValue);

    public static ITypedExpression<int> GetArgumentInt32ValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<int>(argumentName, context.GetArgumentInt32ValueResult(index, argumentName));

    public static ITypedExpression<int> GetArgumentInt32ValueExpression(this FunctionCallContext context, int index, string argumentName, int defaultValue)
        => ProcessArgumentResult<int>(argumentName, context.GetArgumentInt32ValueResult(index, argumentName, defaultValue));

    public static ITypedExpression<long> GetArgumentInt64ValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<long>(argumentName, context.GetArgumentInt64ValueResult(index, argumentName));

    public static ITypedExpression<long> GetArgumentInt64ValueExpression(this FunctionCallContext context, int index, string argumentName, long defaultValue)
        => ProcessArgumentResult<long>(argumentName, context.GetArgumentInt64ValueResult(index, argumentName, defaultValue));

    public static ITypedExpression<decimal> GetArgumentDecimalValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<decimal>(argumentName, context.GetArgumentDecimalValueResult(index, argumentName));

    public static ITypedExpression<decimal> GetArgumentDecimalValueExpression(this FunctionCallContext context, int index, string argumentName, decimal defaultValue)
        => ProcessArgumentResult<decimal>(argumentName, context.GetArgumentDecimalValueResult(index, argumentName, defaultValue));

    public static ITypedExpression<bool> GetArgumentBooleanValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<bool>(argumentName, context.GetArgumentBooleanValueResult(index, argumentName));

    public static ITypedExpression<bool> GetArgumentBooleanValueExpression(this FunctionCallContext context, int index, string argumentName, bool defaultValue)
        => ProcessArgumentResult<bool>(argumentName, context.GetArgumentBooleanValueResult(index, argumentName, defaultValue));

    public static ITypedExpression<DateTime> GetArgumentDateTimeValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<DateTime>(argumentName, context.GetArgumentDateTimeValueResult(index, argumentName));

    public static ITypedExpression<DateTime> GetArgumentDateTimeValueExpression(this FunctionCallContext context, int index, string argumentName, DateTime defaultValue)
        => ProcessArgumentResult<DateTime>(argumentName, context.GetArgumentDateTimeValueResult(index, argumentName, defaultValue));

    public static ITypedExpression<string> GetArgumentStringValueExpression(this FunctionCallContext context, int index, string argumentName)
        => ProcessArgumentResult<string>(argumentName, context.GetArgumentStringValueResult(index, argumentName));

    public static ITypedExpression<string> GetArgumentStringValueExpression(this FunctionCallContext context, int index, string argumentName, string defaultValue)
        => ProcessArgumentResult<string>(argumentName, context.GetArgumentStringValueResult(index, argumentName, defaultValue));

    public static Result<T> GetArgumentExpressionResult<T>(this FunctionCallContext context, int index, string argumentName)
        => GetArgumentValueExpression<T>(context, index, argumentName).EvaluateTyped(context);

    public static Result<T> GetArgumentExpressionResult<T>(this FunctionCallContext context, int index, string argumentName, T? defaultValue)
        => GetArgumentValueExpression(context, index, argumentName, defaultValue).EvaluateTyped(context);

    public static ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueExpression(this FunctionCallContext context, int index, string argumentName)
    {
        var expressions = GetArgumentValueExpression<IEnumerable>(context, index, argumentName).EvaluateTyped(context.Context);

        return new TypedConstantExpression<IEnumerable>(expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) });
    }

    public static IEnumerable<Expression> GetExpressionsArgumentValueResult(this FunctionCallContext context, int index, string argumentName)
    {
        var expressions = GetArgumentValueExpression<IEnumerable>(context, index, argumentName).EvaluateTyped(context.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) };
    }

    private static TypedConstantResultExpression<T> ProcessArgumentResult<T>(string argumentName, Result argumentValueResult, bool useDefaultValue = false, T? defaultValue = default)
    {
        if (!argumentValueResult.IsSuccessful())
        {
            return new TypedConstantResultExpression<T>(Result.FromExistingResult<T>(argumentValueResult));
        }

        var value = argumentValueResult.GetValue();
        if (value is T t)
        {
            return new TypedConstantResultExpression<T>(Result.Success(t));
        }

        if (value is null && useDefaultValue)
        {
            return new TypedConstantResultExpression<T>(Result.Success(defaultValue!));
        }

        if (typeof(T).IsEnum && value is string s)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                return new TypedConstantResultExpression<T>(Result.Success((T)Enum.Parse(typeof(T), s)));
            }
            catch (Exception ex)
            {
                return new TypedConstantResultExpression<T>(Result.Invalid<T>($"{argumentName} value [{s}] could not be converted to {typeof(T).FullName}. Error message: {ex.Message}"));
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        return new TypedConstantResultExpression<T>(Result.Invalid<T>($"{argumentName} is not of type {typeof(T).FullName}"));
    }
}
