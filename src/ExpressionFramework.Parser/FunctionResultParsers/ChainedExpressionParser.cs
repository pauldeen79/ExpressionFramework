namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ChainedExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        return Result<Expression>.Success(new ChainedExpression(
            GetExpressionsArgumentValue(functionParseResult, 0, nameof(ChainedExpression.Expressions), evaluator)
        ));
    }

    public ChainedExpressionParser(IExpressionParser parser) : base(parser, @"Chained")
    {
    }
}

