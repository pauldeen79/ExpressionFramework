namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ContextExpressionParser : IFunctionResultParser, IExpressionResolver
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return !result.IsSuccessful() || result.Status == ResultStatus.Continue
            ? Result<object?>.FromExistingResult(result)
            : result.Value!.Evaluate(context);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == "CONTEXT"
            ? Result<Expression>.Success(new ContextExpression())
            : Result<Expression>.Continue();
}

