namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface INotEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] IExpression FirstExpression { get; }
    [Required][ValidateObject] IExpression SecondExpression { get; }
}
