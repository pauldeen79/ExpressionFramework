namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionResolver
{
    Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);
}
