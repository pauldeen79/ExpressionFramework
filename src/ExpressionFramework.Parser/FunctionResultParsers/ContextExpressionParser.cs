namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ContextExpressionParser : IFunctionResultParser, Contracts.IExpressionParser
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return !result.IsSuccessful() || result.Status == ResultStatus.Continue
            ? Result<object?>.FromExistingResult(result)
            : result.Value!.Evaluate(context);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "CONTEXT")
        {
            return Result<Expression>.Continue();
        }

        return Result<Expression>.Success(new ContextExpression());
    }
}

