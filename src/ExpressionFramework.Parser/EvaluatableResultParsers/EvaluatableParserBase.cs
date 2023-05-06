﻿namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected EvaluatableParserBase(string functionName)
    {
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => functionParseResult.FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? Result<object?>.FromExistingResult(DoParse(functionParseResult, evaluator, parser))
            : Result<object?>.Continue();

    protected abstract Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
