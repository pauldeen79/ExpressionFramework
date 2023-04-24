namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAndExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    ITypedExpression<bool> FirstExpression { get; }
    [Required]
    ITypedExpression<bool> SecondExpression { get; }
}
