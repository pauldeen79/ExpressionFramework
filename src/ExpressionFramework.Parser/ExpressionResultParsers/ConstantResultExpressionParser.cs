namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class ConstantResultExpressionParser : ExpressionParserBase
{
    public ConstantResultExpressionParser() : base(@"ConstantResult")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var constantValueResult = functionParseResult.GetArgumentValueResult<Result>(0, nameof(ConstantExpression.Value), evaluator, parser)
            .EvaluateTyped(functionParseResult.Context);
        
        return constantValueResult.IsSuccessful()
            ? Result<Expression>.Success(new ConstantResultExpression(constantValueResult.Value!))
            : Result<Expression>.FromExistingResult(constantValueResult);
    }
}

