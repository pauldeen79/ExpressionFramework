namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionFrameworkParser
{
    Result<Expression> ParseExpression(FunctionCallContext context);
}
