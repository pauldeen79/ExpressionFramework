namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IBooleanConstantExpression : IExpression
{
    bool Value { get; }
}
