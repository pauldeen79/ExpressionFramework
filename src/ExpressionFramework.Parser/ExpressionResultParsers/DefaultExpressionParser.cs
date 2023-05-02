namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DefaultExpressionParser : ExpressionParserBase
{
    public DefaultExpressionParser() : base(@"Default")
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

        return Result<Expression>.Success((Expression)Activator.CreateInstance(typeof(DefaultExpression<>).MakeGenericType(typeResult.Value!)));
    }
}
