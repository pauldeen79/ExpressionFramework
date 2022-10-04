namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression
{
    int Count { get; }
}
