namespace ExpressionFramework.Parser.Extensions;

public static class FunctionParseResultExtensions
{
    public static ITypedExpression<T> GetArgumentValueExpression<T>(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<T> GetArgumentValueExpression<T>(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T? defaultValue)
        => ProcessArgumentResult(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue), true, defaultValue);

    public static ITypedExpression<int> GetArgumentInt32ValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<int>(argumentName, functionParseResult.GetArgumentInt32ValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<int> GetArgumentInt32ValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, int defaultValue)
        => ProcessArgumentResult<int>(argumentName, functionParseResult.GetArgumentInt32ValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<long> GetArgumentInt64ValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<long>(argumentName, functionParseResult.GetArgumentInt64ValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<long> GetArgumentInt64ValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, long defaultValue)
        => ProcessArgumentResult<long>(argumentName, functionParseResult.GetArgumentInt64ValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<decimal> GetArgumentDecimalValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<decimal>(argumentName, functionParseResult.GetArgumentDecimalValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<decimal> GetArgumentDecimalValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, decimal defaultValue)
        => ProcessArgumentResult<decimal>(argumentName, functionParseResult.GetArgumentDecimalValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<bool> GetArgumentBooleanValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<bool>(argumentName, functionParseResult.GetArgumentBooleanValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<bool> GetArgumentBooleanValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, bool defaultValue)
        => ProcessArgumentResult<bool>(argumentName, functionParseResult.GetArgumentBooleanValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<DateTime> GetArgumentDateTimeValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<DateTime>(argumentName, functionParseResult.GetArgumentDateTimeValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<DateTime> GetArgumentDateTimeValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, DateTime defaultValue)
        => ProcessArgumentResult<DateTime>(argumentName, functionParseResult.GetArgumentDateTimeValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<string> GetArgumentStringValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<string>(argumentName, functionParseResult.GetArgumentStringValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<string> GetArgumentStringValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, string defaultValue)
        => ProcessArgumentResult<string>(argumentName, functionParseResult.GetArgumentStringValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static Result<T> GetArgumentExpressionResult<T>(this FunctionParseResult functionParseResult, int index, string argumentName, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => GetArgumentValueExpression<T>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(context);

    public static Result<T> GetArgumentExpressionResult<T>(this FunctionParseResult functionParseResult, int index, string argumentName, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T? defaultValue)
        => GetArgumentValueExpression(functionParseResult, index, argumentName, evaluator, parser, defaultValue).EvaluateTyped(context);

    public static ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueExpression(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueExpression<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return new TypedConstantExpression<IEnumerable>(expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) });
    }

    public static IEnumerable<Expression> GetExpressionsArgumentValueResult(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueExpression<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

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
