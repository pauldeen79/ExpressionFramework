﻿namespace ExpressionFramework.Parser.AggregatorResultParsers;

public abstract class AggregatorParserBase : IFunctionResultParser
{
    private readonly string _functionName;

    protected AggregatorParserBase(string functionName)
    {
        _functionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => (functionParseResult ?? throw new ArgumentNullException(nameof(functionParseResult))).FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? Result<object?>.FromExistingResult(DoParse(functionParseResult, evaluator, parser))
            : Result<object?>.Continue();

    protected abstract Result<Aggregator> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);
}
