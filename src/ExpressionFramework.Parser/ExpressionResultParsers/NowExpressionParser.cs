namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class NowExpressionParser : ExpressionParserBase
{
    public NowExpressionParser() : base(@"Now")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var dateTimeProvider = functionParseResult.GetArgumentExpressionResult<IDateTimeProvider>(0, nameof(NowExpression.DateTimeProvider), functionParseResult.Context, evaluator, parser, null!);

        return dateTimeProvider.IsSuccessful()
            ? Result<Expression>.Success(new NowExpression(dateTimeProvider.Value))
            : Result<Expression>.FromExistingResult(dateTimeProvider);
    }
}
