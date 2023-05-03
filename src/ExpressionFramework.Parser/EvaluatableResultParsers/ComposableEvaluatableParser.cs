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
        var combinationResult = functionParseResult.GetArgumentValueResult(3, nameof(ComposableEvaluatable.Combination), evaluator, parser, Combination.And).EvaluateTyped();
        var operatorResult = functionParseResult.GetArgumentValueResult<Operator>(1, nameof(ComposableEvaluatable.Operator), evaluator, parser).EvaluateTyped();
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

        var leftExpression = functionParseResult.GetExpressionArgumentValueResult(0, nameof(ComposableEvaluatable.LeftExpression), evaluator, parser);
        var rightExpression = functionParseResult.GetExpressionArgumentValueResult(2, nameof(ComposableEvaluatable.RightExpression), evaluator, parser);

        return Result<Evaluatable>.Success(new ComposableEvaluatable(
            startGroupResult.Value,
            endGroupResult.Value,
            combinationResult.Value,
            leftExpression,
            operatorResult.Value!,
            rightExpression));
    }
}

