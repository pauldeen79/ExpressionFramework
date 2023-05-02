namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DefaultExpressionParser : ExpressionParserBase
{
    public DefaultExpressionParser() : base(@"Default")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var typeName = functionParseResult.FunctionName.GetGenericArguments();
        if (string.IsNullOrEmpty(typeName))
        {
            return Result<Expression>.Invalid("No type defined");
        }

        var type = Type.GetType(typeName);
        if (type == null)
        {
            return Result<Expression>.Invalid($"Unknown type: {typeName}");
        }

        var expression = (Expression)Activator.CreateInstance(typeof(DefaultExpression<>).MakeGenericType(type));
        return Result<Expression>.Success(expression);
    }
}
