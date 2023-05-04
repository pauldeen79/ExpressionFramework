namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class EvaluatableExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var evaluatableResult = functionParseResult.GetArgumentExpressionResult<Evaluatable>(0, nameof(EvaluatableExpression.Condition), functionParseResult.Context, evaluator, parser);

        return evaluatableResult.IsSuccessful()
            ? Result<Expression>.Success(new EvaluatableExpression(
                evaluatableResult.Value!,
                new ConstantResultExpression(functionParseResult.GetArgumentValueResult(1, @"Expression", functionParseResult.Context, evaluator, parser))
                ))
            : Result<Expression>.FromExistingResult(evaluatableResult);
    }

    public EvaluatableExpressionParser() : base(@"Evaluatable")
    {
    }
}

