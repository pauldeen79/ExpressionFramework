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

        return new LeftExpression(
            new TypedDelegateResultExpression<string>(_ => functionParseResult.GetArgumentStringValue(0, nameof(LeftExpression.Expression), evaluator)),
            new TypedDelegateResultExpression<int>(_ => functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), evaluator, _parser))
        ).Evaluate(functionParseResult.Context);
    }
}

