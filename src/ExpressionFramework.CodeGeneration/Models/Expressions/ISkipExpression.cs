namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISkipExpression : IExpression
{
    [Required]
    IExpression CountExpression { get; }
}
