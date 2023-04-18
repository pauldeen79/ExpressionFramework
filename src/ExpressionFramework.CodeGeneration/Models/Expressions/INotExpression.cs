namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression Expression { get; }
}
