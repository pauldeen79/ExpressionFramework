namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IMaxExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    IExpression? SelectorExpression { get; }
}
