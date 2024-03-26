namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] IExpression FirstExpression { get; }
    [Required][ValidateObject] IExpression SecondExpression { get; }
}
