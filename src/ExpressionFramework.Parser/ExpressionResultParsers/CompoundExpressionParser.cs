namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class CompoundExpressionParser : ExpressionParserBase
{
    public CompoundExpressionParser() : base(@"Compound")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var aggregatorArgumentResult = GetArgumentValueResult<Aggregator>(functionParseResult, 2, nameof(CompoundExpression.Aggregator), evaluator, parser);
        var aggregatorResult = aggregatorArgumentResult.EvaluateTyped(functionParseResult.Context);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = GetArgumentValueResult<IFormatProvider>(functionParseResult, 3, nameof(CompoundExpression.FormatProviderExpression), evaluator, parser, CultureInfo.InvariantCulture);
        return Result<Expression>.Success(new CompoundExpression(
                GetExpressionArgumentValueResult(functionParseResult, 0, nameof(CompoundExpression.FirstExpression), evaluator, parser),
                GetExpressionArgumentValueResult(functionParseResult, 1, nameof(CompoundExpression.SecondExpression), evaluator, parser),
                aggregatorResult.Value!,
                formatProviderArgumentResult));
    }
}
