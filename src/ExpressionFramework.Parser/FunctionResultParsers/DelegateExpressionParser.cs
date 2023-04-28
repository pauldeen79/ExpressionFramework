namespace ExpressionFramework.Parser.FunctionResultParsers;

public class DelegateExpressionParser : ExpressionParserBase
{
    public DelegateExpressionParser(IExpressionParser parser) : base(parser, "Delegate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
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

