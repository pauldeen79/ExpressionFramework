namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAllExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression PredicateExpression { get; }
}
