using System.ComponentModel.Design.Serialization;

namespace ExpressionFramework.Parser.FunctionResultParsers;

public class LeftExpressionParser : IFunctionResultParser
{
    private readonly IExpressionParser _parser;

    public LeftExpressionParser(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "LEFT")
        {
            return Result<object?>.Continue();
        }

        var expressionResult = functionParseResult.GetArgumentValue(0, nameof(LeftExpression.Expression), evaluator);
        if (!expressionResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(expressionResult);
        }
        var lengthResult = functionParseResult.GetArgumentValue(1, nameof(LeftExpression.LengthExpression), evaluator);
        if (!lengthResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(lengthResult);
        }

        var expressionValue = expressionResult.Value as string;
        if (expressionValue is null)
        {
            return Result<object?>.Invalid("Expression is not of type string");
        }

        int length = 0;
        if (lengthResult.Value is int i1)
        {
            length = i1;
        }
        else if (lengthResult.Value is string s)
        {
            var parseResult = _parser.Parse(s, functionParseResult.FormatProvider, functionParseResult.Context);
            if (!parseResult.IsSuccessful())
            {
                return Result<object?>.Invalid("Expression is not of type integer");
            }
            else if (parseResult.Value is int i2)
            {
                length = i2;
            }
            else
            {
                return Result<object?>.Invalid("Expression is not of type integer");
            }
        }

        return new LeftExpression(expressionValue, length).Evaluate(functionParseResult.Context);
    }
}

