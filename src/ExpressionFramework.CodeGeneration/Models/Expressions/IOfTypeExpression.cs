namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOfTypeExpression : IExpression
{
    [Required]
    IExpression Expression { get; }
    [Required]
    Type Type { get; }
}
