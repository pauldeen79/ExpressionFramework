namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class AggregateExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var aggregatorArgumentResult = GetArgumentValue<Aggregator>(functionParseResult, 1, nameof(AggregateExpression.Aggregator), evaluator);
        var aggregatorResult = aggregatorArgumentResult.Value.Invoke(functionParseResult.Context);
        if (!aggregatorResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(aggregatorResult);
        }

        var formatProviderArgumentResult = functionParseResult.Arguments.Count() >= 3
            ? GetArgumentValue<IFormatProvider>(functionParseResult, 2, nameof(AggregateExpression.FormatProviderExpression), evaluator)
            : new TypedDelegateResultExpression<IFormatProvider>(_ => Result<IFormatProvider>.Success(CultureInfo.InvariantCulture));
        var formatProviderResult = formatProviderArgumentResult.Value.Invoke(functionParseResult.Context);
        return formatProviderResult.IsSuccessful()
            ? Result<Expression>.Success(new AggregateExpression(GetExpressionsArgumentValue(functionParseResult, 0, nameof(AggregateExpression.Expressions), evaluator), aggregatorResult.Value!, formatProviderResult.Value!))
            : Result<Expression>.FromExistingResult(formatProviderResult);
    }

    public AggregateExpressionParser(IExpressionParser parser) : base(parser, @"Aggregate")
    {
    }
}

