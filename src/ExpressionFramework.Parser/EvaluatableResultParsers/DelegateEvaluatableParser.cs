namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class DelegateEvaluatableParser : EvaluatableParserBase
{
    public DelegateEvaluatableParser() : base(@"DelegateEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentValueResult<Func<bool>>(0, nameof(DelegateExpression.Value), evaluator, parser)
            .EvaluateTyped(functionParseResult.Context);

        return valueResult.IsSuccessful()
            ? Result<Evaluatable>.Success(new DelegateEvaluatable(valueResult.Value!))
            : Result<Evaluatable>.FromExistingResult(valueResult);
    }
}
