namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionResolver
{
    Result<Expression> ParseExpression(FunctionCallContext context);
}
