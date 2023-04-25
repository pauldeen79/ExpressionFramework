namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringLengthExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<string> Expression { get; }
}
