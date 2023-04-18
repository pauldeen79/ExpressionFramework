namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAndExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
}
