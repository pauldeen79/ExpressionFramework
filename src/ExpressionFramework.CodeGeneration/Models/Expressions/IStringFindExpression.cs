namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringFindExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression FindExpression { get; }
}
