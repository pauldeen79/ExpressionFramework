namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TodayExpressionParser : ExpressionParserBase
{
    public TodayExpressionParser() : base(@"Today")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var dateTimeProvider = functionParseResult.GetArgumentExpressionResult<IDateTimeProvider>(0, nameof(TodayExpression.DateTimeProvider), functionParseResult.Context, evaluator, parser, null!);

        return dateTimeProvider.IsSuccessful()
            ? Result<Expression>.Success(new TodayExpression(dateTimeProvider.Value))
            : Result<Expression>.FromExistingResult(dateTimeProvider);
    }
}
