namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DelegateResultExpressionParser : ExpressionParserBase
{
    public DelegateResultExpressionParser() : base(@"DelegateResult")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentExpressionResult<Func<object?, Result<object?>>>(0, nameof(DelegateExpression.Value), functionParseResult.Context, evaluator, parser);

        return valueResult.IsSuccessful()
            ? Result<Expression>.Success(new DelegateResultExpression(valueResult.Value!))
            : Result<Expression>.FromExistingResult(valueResult);
    }
}
