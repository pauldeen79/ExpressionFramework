namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] ITypedExpression<bool> Expression { get; }
}
