namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IRightExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
    [Required]
    ITypedExpression<int> LengthExpression { get; }
}
