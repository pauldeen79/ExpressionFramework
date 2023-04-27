namespace ExpressionFramework.Parser.FunctionResultParsers;

public class ContextExpressionParser : IFunctionResultParser
{
    public Result<object?> Parse(FunctionParseResult functionParseResult)
    {
        if (functionParseResult.FunctionName.ToUpperInvariant() != "CONTEXT")
        {
            return Result<object?>.Continue();
        }

        return new ContextExpression().Evaluate(functionParseResult.Context);
    }
}

