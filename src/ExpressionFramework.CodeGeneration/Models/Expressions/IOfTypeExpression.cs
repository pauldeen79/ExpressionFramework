namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IOfTypeExpression : IExpression
{
    [Required]
    Type Type { get; }
}
