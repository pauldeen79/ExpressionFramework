namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IConstantExpression : IExpression
{
    object? Value { get; }
}
