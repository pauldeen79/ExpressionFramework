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
        var lengthResult = functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), evaluator, _parser);

        return new LeftExpression(
            new TypedConstantResultExpression<string>(expressionResult),
            new TypedConstantResultExpression<int>(lengthResult)
        ).Evaluate(functionParseResult.Context);
    }
}

