namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TypedDelegateExpressionParser : ExpressionParserBase
{
    public TypedDelegateExpressionParser() : base(@"TypedDelegate")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var typeResult = GetGenericType(functionParseResult.FunctionName);
        if (!typeResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(typeResult);
        }

        var valueResult = functionParseResult.GetArgumentValueResult(0, nameof(TypedDelegateExpression<string>.Value), functionParseResult.Context, evaluator, parser);

        if (!valueResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(valueResult);
        }

        try
        {
            return Result<Expression>.Success((Expression)Activator.CreateInstance(typeof(TypedDelegateExpression<>).MakeGenericType(typeResult.Value!), valueResult.Value));
        }
        catch (Exception ex)
        {
            return Result<Expression>.Invalid($"Could not create typed delegate expression. Error: {ex.Message}");
        }
    }
}

