namespace ExpressionFramework.Parser.Contracts;

public interface IExpressionResolver
{
    Result<Expression> Parse(FunctionCallContext context);
}
