namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns an error result")]
public interface IErrorExpression : IExpression
{
    [Required][ValidateObject][Description("Error message to use")] ITypedExpression<string> ErrorMessageExpression { get; }
}
