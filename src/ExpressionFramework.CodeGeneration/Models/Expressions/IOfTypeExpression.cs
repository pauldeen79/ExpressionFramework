namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOfTypeExpression : IExpression
{
    Type Type { get; }
}
