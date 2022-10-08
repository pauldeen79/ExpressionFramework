namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISelectExpression : IExpression
{
    [Required]
    IExpression SelectorExpression { get; }
}
