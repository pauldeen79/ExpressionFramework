namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class SingleEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        throw new NotImplementedException();
    }

    public SingleEvaluatableParser() : base(@"SingleEvaluatable")
    {
    }
}

