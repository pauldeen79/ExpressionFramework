namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class ConstantResultExpressionParser : ExpressionParserBase
{
    public ConstantResultExpressionParser() : base(@"ConstantResult")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var constantValueResult = functionParseResult.GetArgumentExpressionResult<Result>(0, nameof(ConstantExpression.Value), functionParseResult.Context, evaluator, parser);
        
        return constantValueResult.IsSuccessful()
            ? Result<Expression>.Success(new ConstantResultExpression(constantValueResult.Value!))
            : Result<Expression>.FromExistingResult(constantValueResult);
    }
}

