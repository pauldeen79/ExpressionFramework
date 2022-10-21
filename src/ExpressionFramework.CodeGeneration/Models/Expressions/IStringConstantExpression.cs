namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConstantExpression : IExpression
{
    string Value { get; }
}
