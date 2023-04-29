namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEvaluatableExpression : IExpression, ITypedExpression<bool>
{
    [Required]
    IEvaluatable Condition { get; }
    [Required]
    IExpression Expression { get; }
}
