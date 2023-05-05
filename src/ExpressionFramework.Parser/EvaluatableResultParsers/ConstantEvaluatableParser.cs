﻿namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public class ConstantEvaluatableParser : EvaluatableParserBase
{
    public ConstantEvaluatableParser() : base(@"ConstantEvaluatable")
    {
    }

    protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentBooleanValueResult(0, nameof(ConstantEvaluatable.Value), functionParseResult.Context, evaluator, parser);

        return valueResult.IsSuccessful()
            ? Result<Evaluatable>.Success(new ConstantEvaluatable(valueResult.Value!))
            : Result<Evaluatable>.FromExistingResult(valueResult);
    }
}

