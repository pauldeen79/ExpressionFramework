namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DelegateExpressionParser : ExpressionParserBase
{
    public DelegateExpressionParser() : base("Delegate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueArgumentResult = GetArgumentValueResult<Func<object?, object?>>(functionParseResult, 0, nameof(DelegateExpression.Value), evaluator, parser);
        var valueResult = valueArgumentResult.EvaluateTyped(functionParseResult.Context);

        return valueResult.IsSuccessful()
            ? Result<Expression>.Success(new DelegateExpression(valueResult.Value!))
            : Result<Expression>.FromExistingResult(valueResult);
    }
}

