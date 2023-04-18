namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ILeftExpression : IExpression
{
    [Required]
    ITypedExpression<string> Expression { get; }
    [Required]
    ITypedExpression<int> LengthExpression { get; }
}
