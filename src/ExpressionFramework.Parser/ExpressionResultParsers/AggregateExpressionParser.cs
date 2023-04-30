namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var aggregatorArgumentResult = GetArgumentValue<Aggregator>(functionParseResult, 1, nameof(AggregateExpression.Aggregator), evaluator);
        var aggregatorResult = aggregatorArgumentResult.Value.Invoke(functionParseResult.Context);
        return aggregatorResult.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(GetExpressionsArgumentValue(functionParseResult, 0, nameof(AggregateExpression.Expressions), evaluator), aggregatorResult.Value!))
            : Result<Expression>.FromExistingResult(aggregatorResult);
    }

    public AggregateExpressionParser(IExpressionParser parser) : base(parser, @"Aggregate")
    {
    }
}

