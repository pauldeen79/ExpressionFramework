namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionParser
{
    Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);
}
