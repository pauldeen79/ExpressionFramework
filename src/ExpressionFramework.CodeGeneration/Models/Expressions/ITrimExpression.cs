namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimExpression : IExpression, ITypedExpression<string>
{
    [Required]
    ITypedExpression<string> Expression { get; }
    ITypedExpression<char[]>? TrimCharsExpression { get; }
}
