namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class ConstantExpressionParser : ExpressionParserBase
{
    public ConstantExpressionParser() : base("Constant")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var constantValueResult = functionParseResult.GetArgumentValueResult(0, nameof(ConstantExpression.Value), functionParseResult.Context, evaluator, parser);
        return constantValueResult.IsSuccessful()
            ? Result<Expression>.Success(new ConstantExpression(constantValueResult.Value))
            : Result<Expression>.FromExistingResult(constantValueResult);
    }
}

