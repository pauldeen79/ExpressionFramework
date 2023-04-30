﻿namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunctionResultParser
{
    private readonly string _functionName;
    protected IExpressionParser Parser { get; }

    protected EvaluatableParserBase(IExpressionParser parser, string functionName)
    {
        Parser = parser;
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? Result<object?>.FromExistingResult(DoParse(functionParseResult, evaluator), value => value)
            : Result<object?>.Continue();

    protected abstract Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);
}
