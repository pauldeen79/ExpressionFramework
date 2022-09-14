namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IErrorExpression : IExpression
{
    string ErrorMessage { get; }
}
