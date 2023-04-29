namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ComposableEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        throw new NotImplementedException();
    }

    public ComposableEvaluatableParser() : base(@"ComposableEvaluatable")
    {
    }
}

