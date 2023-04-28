namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ConstantExpressionParser : IFunctionResultParser, IExpressionResolver
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value!.Evaluate(context)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "CONSTANT")
        {
            return Result<Expression>.Continue();
        }

        var constantValueResult = functionParseResult.GetArgumentValue(0, nameof(ConstantExpression.Value), functionParseResult.Context, evaluator);
        return constantValueResult.IsSuccessful()
            ? Result<Expression>.Success(new ConstantExpression(constantValueResult.Value))
            : Result<Expression>.FromExistingResult(constantValueResult);
    }
}

