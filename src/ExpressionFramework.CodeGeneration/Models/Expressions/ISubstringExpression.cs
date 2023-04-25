namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISubstringExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
    [Required]
    ITypedExpression<int> IndexExpression { get; }
    ITypedExpression<int>? LengthExpression { get; }
}
