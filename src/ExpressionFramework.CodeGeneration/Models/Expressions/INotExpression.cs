namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    ITypedExpression<bool> Expression { get; }
}
