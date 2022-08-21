namespace ExpressionFramework.Abstractions.DomainModel;

public interface IExpression
{
    IExpressionFunction? Function { get; }
    IExpression? InnerExpression { get; }
    IExpressionBuilder ToBuilder();
}
