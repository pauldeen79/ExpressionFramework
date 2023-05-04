namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class CompoundExpressionParser : ExpressionParserBase
{
    public CompoundExpressionParser() : base(@"Compound")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorResult = functionParseResult.GetArgumentExpressionResult<Aggregator>(2, nameof(CompoundExpression.Aggregator), functionParseResult.Context, evaluator, parser);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = functionParseResult.GetArgumentValueExpression<IFormatProvider>(3, nameof(CompoundExpression.FormatProviderExpression), evaluator, parser, CultureInfo.InvariantCulture);
        return Result<Expression>.Success(new CompoundExpression(
            functionParseResult.GetExpressionArgumentValueExpression(0, nameof(CompoundExpression.FirstExpression), evaluator, parser),
            functionParseResult.GetExpressionArgumentValueExpression(1, nameof(CompoundExpression.SecondExpression), evaluator, parser),
            aggregatorResult.Value!,
            formatProviderArgumentResult));
    }
}
