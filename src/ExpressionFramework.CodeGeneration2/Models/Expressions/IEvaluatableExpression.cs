namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEvaluatableExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] IEvaluatable Condition { get; }
    [Required][ValidateObject] IExpression Expression { get; }
}
