namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class DelegateEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        throw new NotImplementedException();
    }

    public DelegateEvaluatableParser() : base(@"DelegateEvaluatable")
    {
    }
}

