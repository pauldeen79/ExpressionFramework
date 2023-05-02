namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DelegateExpressionParser : ExpressionParserBase
{
    public DelegateExpressionParser() : base("Delegate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var delegateValueResult = functionParseResult.GetArgumentValueResult(0, nameof(DelegateExpression.Value), functionParseResult.Context, evaluator, parser);
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

