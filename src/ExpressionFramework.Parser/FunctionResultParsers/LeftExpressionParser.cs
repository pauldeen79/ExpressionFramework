namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : IFunctionResultParser
{
    private readonly IExpressionParser _parser;

    public LeftExpressionParser(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "LEFT")
        {
            return Result<object?>.Continue();
        }

        var expressionResult = functionParseResult.GetArgumentStringValue(0, nameof(LeftExpression.Expression), evaluator);
        if (!expressionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(expressionResult);
        }

        var lengthResult = functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), evaluator, _parser);
        if (!lengthResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(lengthResult);
        }

        return new LeftExpression(expressionResult.Value!, lengthResult.Value).Evaluate(functionParseResult.Context);
    }
}

