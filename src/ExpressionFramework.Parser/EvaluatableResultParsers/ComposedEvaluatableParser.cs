namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ComposedEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        throw new NotImplementedException();
    }

    public ComposedEvaluatableParser() : base(@"ComposedEvaluatable")
    {
    }
}

