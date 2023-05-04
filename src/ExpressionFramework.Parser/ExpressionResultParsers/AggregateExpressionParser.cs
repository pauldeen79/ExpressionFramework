namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    public AggregateExpressionParser() : base(@"Aggregate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorResult = functionParseResult.GetArgumentExpressionResult<Aggregator>(1, nameof(AggregateExpression.Aggregator), functionParseResult.Context, evaluator, parser);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderResult = functionParseResult.GetArgumentExpressionResult<IFormatProvider>(2, nameof(AggregateExpression.FormatProviderExpression), functionParseResult.Context, evaluator, parser, CultureInfo.InvariantCulture);

        return formatProviderResult.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(functionParseResult.GetExpressionsArgumentValueResult(0, nameof(AggregateExpression.Expressions), evaluator, parser), aggregatorResult.Value!, formatProviderResult.Value!))
            : Result<Expression>.FromExistingResult(formatProviderResult);
    }
}
