namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    public AggregateExpressionParser() : base(@"Aggregate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorArgumentResult = functionParseResult.GetArgumentValueResult<Aggregator>(1, nameof(AggregateExpression.Aggregator), evaluator, parser);
        var aggregatorResult = aggregatorArgumentResult.EvaluateTyped(functionParseResult.Context);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = functionParseResult.GetArgumentValueResult<IFormatProvider>(2, nameof(AggregateExpression.FormatProviderExpression), evaluator, parser, CultureInfo.InvariantCulture);
        var formatProviderResult = formatProviderArgumentResult.EvaluateTyped(functionParseResult.Context);
        return formatProviderResult.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(functionParseResult.GetExpressionsArgumentValueResult(0, nameof(AggregateExpression.Expressions), evaluator, parser), aggregatorResult.Value!, formatProviderResult.Value!))
            : Result<Expression>.FromExistingResult(formatProviderResult);
    }
}
