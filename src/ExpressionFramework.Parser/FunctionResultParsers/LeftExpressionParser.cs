namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : ExpressionParserBase
{
    public LeftExpressionParser(IExpressionParser parser) : base(parser, "Left")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => Result<Expression>.Success(new LeftExpression(
            new TypedDelegateResultExpression<string>(context => functionParseResult.GetArgumentStringValue(0, nameof(LeftExpression.Expression), context, evaluator)),
            new TypedDelegateResultExpression<int>(context => functionParseResult.GetArgumentInt32Value(1, nameof(LeftExpression.LengthExpression), context, evaluator, Parser))
        ));
}

