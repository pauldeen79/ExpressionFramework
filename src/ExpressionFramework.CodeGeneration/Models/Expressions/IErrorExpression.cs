namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IErrorExpression : IExpression
{
    [Required]
    IExpression ErrorMessageExpression { get; }
}
