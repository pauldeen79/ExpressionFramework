namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IRightExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> LengthExpression { get; }
}
