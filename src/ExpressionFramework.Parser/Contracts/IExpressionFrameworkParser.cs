namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionFrameworkParser
{
    Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);
}
