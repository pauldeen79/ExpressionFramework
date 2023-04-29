namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class EvaluatableExpressionParser : ExpressionParserBase
{
    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var evaluatableArgumentResult = GetArgumentValue<Evaluatable>(functionParseResult, 1, nameof(EvaluatableExpression.Condition), evaluator);
        var result = evaluatableArgumentResult.Value.Invoke(functionParseResult.Context);
        return result.IsSuccessful()
            ? Result<Expression>.Success(new EvaluatableExpression(
                result.Value!,
                new DelegateResultExpression(_ => functionParseResult.GetArgumentValue(0, @"Expression", functionParseResult.Context, evaluator))
                ))
            : Result<Expression>.FromExistingResult(result);
    }

    public EvaluatableExpressionParser(IExpressionParser parser) : base(parser, @"Evaluatable")
    {
    }
}

