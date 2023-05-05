namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class TypedDelegateResultExpressionParser : ExpressionParserBase
{
    public TypedDelegateResultExpressionParser() : base(@"TypedDelegateResult")
    {
    }

    protected override bool IsNameValid(string functionName) => base.IsNameValid(functionName.WithoutGenerics());

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        throw new NotImplementedException();
    }
}
