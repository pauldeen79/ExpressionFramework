namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ConstantEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        throw new NotImplementedException();
    }

    public ConstantEvaluatableParser() : base(@"ConstantEvaluatable")
    {
    }
}

