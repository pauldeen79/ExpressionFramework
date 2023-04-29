namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var aggregatorArgumentResult = GetArgumentValue<Aggregator>(functionParseResult, 0, nameof(AggregateExpression.Aggregator), evaluator);
        var result = aggregatorArgumentResult.Value.Invoke(functionParseResult.Context);
        return result.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(GetExpressionsArgumentValue(functionParseResult, 0, nameof(AggregateExpression.Expressions), evaluator), result.Value!))
            : Result<Expression>.FromExistingResult(result);
    }

    public AggregateExpressionParser(IExpressionParser parser) : base(parser, @"Aggregate")
    {
    }
}

