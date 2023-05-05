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

    protected Result<Expression> ParseTypedExpression(Type expressionType, string argumentName, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();
        if (!typeResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(typeResult);
        }

        var valueResult = functionParseResult.GetArgumentValueResult(0, argumentName, functionParseResult.Context, evaluator, parser);
        if (!valueResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(valueResult);
        }

        try
        {
            return Result<Expression>.Success((Expression)Activator.CreateInstance(expressionType.MakeGenericType(typeResult.Value!), valueResult.Value));
        }
        catch (Exception ex)
        {
            return Result<Expression>.Invalid($"Could not create {expressionType.Name.Replace("`1", string.Empty)}. Error: {ex.Message}");
        }
    }
}
