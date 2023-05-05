namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TypedConstantExpressionParser : ExpressionParserBase
{
    public TypedConstantExpressionParser() : base(@"TypedConstant")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();
        if (!typeResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(typeResult);
        }

        var valueResult = functionParseResult.GetArgumentValueResult(0, nameof(TypedConstantExpression<string>.Value), functionParseResult.Context, evaluator, parser);
        if (!valueResult.IsSuccessful())
        {
            return Result<Expression>.FromExistingResult(valueResult);
        }

        try
        {
            return Result<Expression>.Success((Expression)Activator.CreateInstance(typeof(TypedConstantExpression<>).MakeGenericType(typeResult.Value!), valueResult.Value));
        }
        catch (Exception ex)
        {
            return Result<Expression>.Invalid($"Could not create typed constant expression. Error: {ex.Message}");
        }
    }
}
