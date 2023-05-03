namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class CompoundExpressionParser : ExpressionParserBase
{
    public CompoundExpressionParser() : base(@"Compound")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorArgumentResult = functionParseResult.GetArgumentValueResult<Aggregator>(2, nameof(CompoundExpression.Aggregator), evaluator, parser);
        var aggregatorResult = aggregatorArgumentResult.EvaluateTyped(functionParseResult.Context);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = functionParseResult.GetArgumentValueResult<IFormatProvider>(3, nameof(CompoundExpression.FormatProviderExpression), evaluator, parser, CultureInfo.InvariantCulture);
        return Result<Expression>.Success(new CompoundExpression(
                functionParseResult.GetExpressionArgumentValueResult(0, nameof(CompoundExpression.FirstExpression), evaluator, parser),
                functionParseResult.GetExpressionArgumentValueResult(1, nameof(CompoundExpression.SecondExpression), evaluator, parser),
                aggregatorResult.Value!,
                formatProviderArgumentResult));
    }
}
