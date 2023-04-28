namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ContextExpressionParser : ExpressionParserBase
{
    public ContextExpressionParser(IExpressionParser parser) : base(parser, "Context")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == "CONTEXT"
            ? Result<Expression>.Success(new ContextExpression())
            : Result<Expression>.Continue();
}

