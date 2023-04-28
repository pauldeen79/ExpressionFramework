namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : IFunctionResultParser, Contracts.IExpressionParser
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        if (!result.IsSuccessful() || result.Status == ResultStatus.Continue)
        {
            return Result<object?>.FromExistingResult(result);
        }
        return result.Value!.Evaluate(context);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "LEFT")
        {
            return Result<Expression>.Continue();
        }
        return Result<Expression>.Success(new LeftExpression(
            new TypedDelegateResultExpression<string>(context => functionParseResult.GetArgumentStringValue(0, nameof(LeftExpression.Expression), context, evaluator)),
            new TypedDelegateResultExpression<int>(context => functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), context, evaluator, _parser))
        ));
    }

    public LeftExpressionParser(CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
    {
        _parser = parser;
    }

    private readonly CrossCutting.Utilities.Parsers.Contracts.IExpressionParser _parser;
}

