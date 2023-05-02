namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class EvaluatableExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var evaluatableArgumentResult = GetArgumentValueResult<Evaluatable>(functionParseResult, 1, nameof(EvaluatableExpression.Condition), evaluator, parser);
        var result = evaluatableArgumentResult.EvaluateTyped(functionParseResult.Context);
        return result.IsSuccessful()
            ? Result<Expression>.Success(new EvaluatableExpression(
                result.Value!,
                new DelegateResultExpression(_ => functionParseResult.GetArgumentValueResult(0, @"Expression", functionParseResult.Context, evaluator, parser))
                ))
            : Result<Expression>.FromExistingResult(result);
    }

    public EvaluatableExpressionParser() : base(@"Evaluatable")
    {
    }
}

