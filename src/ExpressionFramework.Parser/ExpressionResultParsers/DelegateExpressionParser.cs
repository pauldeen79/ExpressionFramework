﻿namespace ExpressionFramework.Parser.ExpressionResultParsers;

public class DelegateExpressionParser : ExpressionParserBase
{
    public DelegateExpressionParser() : base("Delegate")
    {
    }

    protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var valueResult = functionParseResult.GetArgumentValueResult<Func<object?, object?>>(0, nameof(DelegateExpression.Value), evaluator, parser)
            .EvaluateTyped(functionParseResult.Context);

        return valueResult.IsSuccessful()
            ? Result<Expression>.Success(new DelegateExpression(valueResult.Value!))
            : Result<Expression>.FromExistingResult(valueResult);
    }
}

