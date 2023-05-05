namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class SingleEvaluatableParser : EvaluatableParserBase
{
    public SingleEvaluatableParser() : base(@"SingleEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var operatorResult = functionParseResult.GetArgumentExpressionResult<Operator>(1, nameof(SingleEvaluatable.Operator), functionParseResult.Context, evaluator, parser);
        if (!operatorResult.IsSuccessful())
        {
            return Result<Evaluatable>.FromExistingResult(operatorResult);
        }

        return Result<Evaluatable>.Success(new SingleEvaluatable(
            functionParseResult.GetExpressionArgumentValueExpression(0, nameof(SingleEvaluatable.LeftExpression), evaluator, parser),
            operatorResult.Value!,
            functionParseResult.GetExpressionArgumentValueExpression(2, nameof(SingleEvaluatable.RightExpression), evaluator, parser)));
    }
}

