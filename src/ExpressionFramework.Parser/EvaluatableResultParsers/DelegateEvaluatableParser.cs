namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class DelegateEvaluatableParser : EvaluatableParserBase
{
    public DelegateEvaluatableParser() : base(@"DelegateEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentExpressionResult<Func<bool>>(0, nameof(DelegateExpression.Value), functionParseResult.Context, evaluator, parser);

        return valueResult.IsSuccessful()
            ? Result<Evaluatable>.Success(new DelegateEvaluatable(valueResult.Value!))
            : Result<Evaluatable>.FromExistingResult(valueResult);
    }
}
