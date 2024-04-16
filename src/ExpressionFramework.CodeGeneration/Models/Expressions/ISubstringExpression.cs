namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISubstringExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<int> IndexExpression { get; }
    [ValidateObject] ITypedExpression<int>? LengthExpression { get; }
}
