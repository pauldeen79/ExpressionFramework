namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IErrorExpression : IExpression
{
    [Required]
    ITypedExpression<string> ErrorMessageExpression { get; }
}
