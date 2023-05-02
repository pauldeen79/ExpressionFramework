namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    public AggregateExpressionParser() : base(@"Aggregate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorArgumentResult = GetArgumentValueResult<Aggregator>(functionParseResult, 1, nameof(AggregateExpression.Aggregator), evaluator, parser);
        var aggregatorResult = aggregatorArgumentResult.Value.Invoke(functionParseResult.Context);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = GetArgumentValueResult<IFormatProvider>(functionParseResult, 2, nameof(AggregateExpression.FormatProviderExpression), evaluator, parser, CultureInfo.InvariantCulture);
        var formatProviderResult = formatProviderArgumentResult.Value.Invoke(functionParseResult.Context);
        return formatProviderResult.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(GetExpressionsArgumentValueResult(functionParseResult, 0, nameof(AggregateExpression.Expressions), evaluator, parser), aggregatorResult.Value!, formatProviderResult.Value!))
            : Result<Expression>.FromExistingResult(formatProviderResult);
    }
}
