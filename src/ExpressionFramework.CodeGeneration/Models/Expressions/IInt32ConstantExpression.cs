namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInt32ConstantExpression : IExpression
{
    int Value { get; }
}
