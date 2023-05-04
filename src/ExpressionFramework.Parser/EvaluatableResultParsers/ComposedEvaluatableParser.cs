namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ComposedEvaluatableParser : EvaluatableParserBase
{
    public ComposedEvaluatableParser() : base(@"ComposedEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentExpressionResult<IEnumerable<ComposableEvaluatable>>(0, nameof(ComposedEvaluatable.Conditions), functionParseResult.Context, evaluator, parser);

        return valueResult.IsSuccessful()
            ? Result<Evaluatable>.Success(new ComposedEvaluatable(valueResult.Value!))
            : Result<Evaluatable>.FromExistingResult(valueResult);
    }
}

