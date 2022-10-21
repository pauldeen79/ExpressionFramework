namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAllExpression : IExpression
{
    IExpression PredicateExpression { get; }
}
