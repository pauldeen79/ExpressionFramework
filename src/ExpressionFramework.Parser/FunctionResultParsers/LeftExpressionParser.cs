using System.Globalization;

namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : IFunctionResultParser
{
    public Result<object?> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "LEFT")
        {
            return Result<object?>.Continue();
        }

        var expressionResult = functionParseResult.GetArgumentValueString(0, nameof(LeftExpression.Expression), evaluator);
        if (!expressionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(expressionResult);
        }

        var lengthResult = functionParseResult.GetArgumentValueInt32(1, nameof(LeftExpression.LengthExpression), evaluator);
        if (!lengthResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(lengthResult);
        }

        return new LeftExpression(expressionResult.Value!, lengthResult.Value).Evaluate(functionParseResult.Context);
    }
}

