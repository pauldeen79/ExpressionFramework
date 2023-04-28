namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ConstantExpressionParser : ExpressionParserBase
{
    public ConstantExpressionParser(IExpressionParser parser) : base(parser, "Constant")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var constantValueResult = functionParseResult.GetArgumentValue(0, nameof(ConstantExpression.Value), functionParseResult.Context, evaluator);
        return constantValueResult.IsSuccessful()
            ? Result<Expression>.Success(new ConstantExpression(constantValueResult.Value))
            : Result<Expression>.FromExistingResult(constantValueResult);
    }
}

