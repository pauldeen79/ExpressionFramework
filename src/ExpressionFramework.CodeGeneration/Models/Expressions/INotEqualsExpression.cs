namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
