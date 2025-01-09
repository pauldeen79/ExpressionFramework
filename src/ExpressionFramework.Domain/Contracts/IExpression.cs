namespace ExpressionFramework.Domain.Contracts;

public interface IExpression
{
    Result<object?> Evaluate(object? context);
    ExpressionBuilder ToBuilder();
}
