namespace ExpressionFramework.Parser.Extensions;

public static class FunctionParseResultExtensions
{
    public static ITypedExpression<T> GetArgumentValueResult<T>(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    public static ITypedExpression<T> GetArgumentValueResult<T>(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T defaultValue)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    public static ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueResult(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return new TypedConstantExpression<IEnumerable>(expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) });
    }

    public static Expression GetExpressionArgumentValueResult(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressionResult = GetArgumentValueResult<object?>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressionResult.IsSuccessful()
            ? new ConstantExpression(expressionResult.Value)
            : new ConstantResultExpression(expressionResult);
    }

    public static IEnumerable<Expression> GetExpressionsArgumentValueResult(this FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) };
    }

    private static ITypedExpression<T> ProcessArgumentResult<T>(string argumentName, Result<object?> argumentValueResult)
    {
        if (!argumentValueResult.IsSuccessful())
        {
            return new TypedConstantResultExpression<T>(Result<T>.FromExistingResult(argumentValueResult));
        }

        if (argumentValueResult.Value is T t)
        {
            return new TypedConstantResultExpression<T>(Result<T>.Success(t));
        }

        if (typeof(T).IsEnum && argumentValueResult.Value is string s)
        {
            try
            {
                return new TypedConstantResultExpression<T>(Result<T>.Success((T)Enum.Parse(typeof(T), s)));
            }
            catch (Exception ex)
            {
                return new TypedConstantResultExpression<T>(Result<T>.Invalid($"{argumentName} value [{s}] could not be converted to {typeof(T).FullName}. Error message: {ex.Message}"));
            }
        }

        return new TypedConstantResultExpression<T>(Result<T>.Invalid($"{argumentName} is not of type {typeof(T).FullName}"));
    }
}
