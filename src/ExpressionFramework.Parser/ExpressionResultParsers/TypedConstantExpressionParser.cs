namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TypedConstantExpressionParser : ExpressionParserBase
{
    public TypedConstantExpressionParser() : base(@"TypedConstant")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ParseTypedExpression(typeof(TypedConstantExpression<>), nameof(TypedConstantExpression<object>.Value), functionParseResult, evaluator, parser);
}
