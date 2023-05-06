namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
