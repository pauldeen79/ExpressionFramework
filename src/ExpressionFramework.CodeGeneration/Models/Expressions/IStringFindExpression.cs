namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringFindExpression : IExpression, ITypedExpression<int>
{
    [Required]
    ITypedExpression<string> Expression { get; }
    [Required]
    ITypedExpression<string> FindExpression { get; }
}
