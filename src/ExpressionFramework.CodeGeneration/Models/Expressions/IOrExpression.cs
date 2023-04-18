namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOrExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
