namespace ExpressionFramework.Parser.FunctionResultParsers;

public class DelegateExpressionParser : IFunctionResultParser, IExpressionResolver
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
        if (functionParseResult.FunctionName.ToUpperInvariant() != "DELEGATE")
        {
            return Result<Expression>.Continue();
        }

        var delegateValueResult = functionParseResult.GetArgumentValue(0, nameof(DelegateExpression.Value), functionParseResult.Context, evaluator);
        if (!delegateValueResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(delegateValueResult);
        }

        if (delegateValueResult.Value is not Func<object?, object?> dlg)
        {
            return Result<Expression>.Invalid("Value is not of type delegate (Func<object?, object?>)");
        }

        return Result<Expression>.Success(new DelegateExpression(dlg));
    }
}

