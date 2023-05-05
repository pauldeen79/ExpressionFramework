namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TypedDelegateExpressionParser : ExpressionParserBase
{
    public TypedDelegateExpressionParser() : base(@"TypedDelegate")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        => ParseTypedExpression(typeof(TypedDelegateExpression<>), nameof(TypedDelegateExpression<object>.Value), functionParseResult, evaluator, parser);
}
