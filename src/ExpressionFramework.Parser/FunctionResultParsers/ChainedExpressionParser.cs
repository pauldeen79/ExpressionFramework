namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ChainedExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var expressions = GetArgumentValue<IEnumerable>(functionParseResult, 0, nameof(ChainedExpression.Expressions), evaluator).Value.Invoke(functionParseResult.Context);

        return Result<Expression>.Success(new ChainedExpression(
            expressions.IsSuccessful()
                ? expressions.Value!.OfType<object>().Select(x => new DelegateExpression(_ => x)).Cast<Expression>()
                : new Expression[] { new DelegateResultExpression(_ => Result<object?>.FromExistingResult(expressions)) } ));
    }

    public ChainedExpressionParser(IExpressionParser parser) : base(parser, @"Chained")
    {
    }
}

