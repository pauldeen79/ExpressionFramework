namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAndExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject] ITypedExpression<bool> FirstExpression { get; }
    [Required][ValidateObject] ITypedExpression<bool> SecondExpression { get; }
}
