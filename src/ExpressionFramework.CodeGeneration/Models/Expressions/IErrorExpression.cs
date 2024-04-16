namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IErrorExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<string> ErrorMessageExpression { get; }
}
