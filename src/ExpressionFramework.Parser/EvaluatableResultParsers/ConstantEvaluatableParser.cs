namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ConstantEvaluatableParser : EvaluatableParserBase
{
    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        var valueResult = functionParseResult.GetArgumentBooleanValue(0, nameof(ConstantEvaluatable.Value), functionParseResult.Context, evaluator, Parser);
        return valueResult.IsSuccessful()
            ? Result<Evaluatable>.Success(new ConstantEvaluatable(valueResult.Value!))
            : Result<Evaluatable>.FromExistingResult(valueResult);
    }

    public ConstantEvaluatableParser(IExpressionParser parser) : base(parser, @"ConstantEvaluatable")
    {
    }
}

