namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class EvaluatableExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var evaluatableResult = functionParseResult.GetArgumentValueResult<Evaluatable>(1, nameof(EvaluatableExpression.Condition), evaluator, parser)
            .EvaluateTyped(functionParseResult.Context);

        return evaluatableResult.IsSuccessful()
            ? Result<Expression>.Success(new EvaluatableExpression(
                evaluatableResult.Value!,
                new ConstantResultExpression(functionParseResult.GetArgumentValueResult(0, @"Expression", functionParseResult.Context, evaluator, parser))
                ))
            : Result<Expression>.FromExistingResult(evaluatableResult);
    }

    public EvaluatableExpressionParser() : base(@"Evaluatable")
    {
    }
}

