namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ContextExpressionParser : IFunctionResultParser, IExpressionResolver
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value!.Evaluate(context)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == "CONTEXT"
            ? Result<Expression>.Success(new ContextExpression())
            : Result<Expression>.Continue();
}

