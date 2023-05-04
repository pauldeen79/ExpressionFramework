namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ComposableEvaluatableParser : EvaluatableParserBase
{
    public ComposableEvaluatableParser() : base(@"ComposableEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var startGroupResult = functionParseResult.GetArgumentBooleanValueResult(4, nameof(ComposableEvaluatable.StartGroup), functionParseResult.Context, evaluator, parser, false);
        var endGroupResult = functionParseResult.GetArgumentBooleanValueResult(5, nameof(ComposableEvaluatable.EndGroup), functionParseResult.Context, evaluator, parser, false);
        var combinationResult = functionParseResult.GetArgumentExpressionResult(3, nameof(ComposableEvaluatable.Combination), functionParseResult.Context, evaluator, parser, Combination.And);
        var operatorResult = functionParseResult.GetArgumentExpressionResult<Operator>(1, nameof(ComposableEvaluatable.Operator), functionParseResult.Context, evaluator, parser);
        var error = new Result[]
        {
            startGroupResult,
            endGroupResult,
            combinationResult,
            operatorResult,
        }.FirstOrDefault(x => !x.IsSuccessful());

        if (error != null)
        {
            return Result<Evaluatable>.FromExistingResult(error);
        }

        var leftExpression = functionParseResult.GetExpressionArgumentValueExpression(0, nameof(ComposableEvaluatable.LeftExpression), evaluator, parser);
        var rightExpression = functionParseResult.GetExpressionArgumentValueExpression(2, nameof(ComposableEvaluatable.RightExpression), evaluator, parser);

        return Result<Evaluatable>.Success(new ComposableEvaluatable(
            startGroupResult.Value,
            endGroupResult.Value,
            combinationResult.Value,
            leftExpression,
            operatorResult.Value!,
            rightExpression));
    }
}

