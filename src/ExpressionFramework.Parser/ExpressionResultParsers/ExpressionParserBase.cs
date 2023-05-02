﻿namespace ExpressionFramework.Parser.ExpressionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;

    protected ExpressionParserBase(string functionName)
    {
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = Parse(functionParseResult, evaluator, parser);

        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value?.Evaluate(context) ?? Result<object?>.Success(null)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => IsNameValid(functionParseResult.FunctionName)
            ? DoParse(functionParseResult, evaluator, parser)
            : Result<Expression>.Continue();

    protected virtual bool IsNameValid(string functionName)
        => functionName.ToUpperInvariant() == _functionName.ToUpperInvariant();

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);

    protected ITypedExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser));

    protected ITypedExpression<T> GetArgumentValueResult<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser, T defaultValue)
        => ProcessArgumentResult<T>(argumentName, functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser, defaultValue));

    protected ITypedExpression<IEnumerable> GetTypedExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return new TypedConstantExpression<IEnumerable>(expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) });
    }

    protected Expression GetExpressionArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressionResult = GetArgumentValueResult<object?>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressionResult.IsSuccessful()
            ? new ConstantExpression(expressionResult.Value)
            : new ConstantResultExpression(expressionResult);
    }

    protected IEnumerable<Expression> GetExpressionsArgumentValueResult(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var expressions = GetArgumentValueResult<IEnumerable>(functionParseResult, index, argumentName, evaluator, parser).EvaluateTyped(functionParseResult.Context);

        return expressions.IsSuccessful()
            ? expressions.Value!.OfType<object>().Select(x => new ConstantExpression(x))
            : new Expression[] { new ConstantResultExpression(expressions) };
    }

    protected static Result<Type> GetGenericType(string functionName)
    {
        var typeName = functionName.GetGenericArguments();
        if (string.IsNullOrEmpty(typeName))
        {
            return Result<Type>.Invalid("No type defined");
        }

        var type = Type.GetType(typeName);
        return type != null
            ? Result<Type>.Success(type)
            : Result<Type>.Invalid($"Unknown type: {typeName}");
    }

    private static ITypedExpression<T> ProcessArgumentResult<T>(string argumentName, Result<object?> argumentValueResult)
    {
        if (!argumentValueResult.IsSuccessful())
        {
            return new TypedConstantResultExpression<T>(Result<T>.FromExistingResult(argumentValueResult));
        }

        return argumentValueResult.Value is T t
            ? new TypedConstantResultExpression<T>(Result<T>.Success(t))
            : (ITypedExpression<T>)new TypedConstantResultExpression<T>(Result<T>.Invalid($"{argumentName} is not of type {typeof(T).FullName}"));
    }
}
