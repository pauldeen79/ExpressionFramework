namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILeftExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> LengthExpression { get; }
}
