namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : IFunctionResultParser, IExpressionResolver
{
    public LeftExpressionParser(IExpressionParser parser)
    {
        _parser = parser;
    }

    private readonly IExpressionParser _parser;

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
        => functionParseResult.FunctionName.ToUpperInvariant() == "LEFT"
            ? Result<Expression>.Success(new LeftExpression(
                new TypedDelegateResultExpression<string>(context => functionParseResult.GetArgumentStringValue(0, nameof(LeftExpression.Expression), context, evaluator)),
                new TypedDelegateResultExpression<int>(context => functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), context, evaluator, _parser))
            ))
            : Result<Expression>.Continue();
}

