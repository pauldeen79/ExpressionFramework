namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    [Required][ValidateObject] ITypedExpression<string> ErrorMessageExpression { get; }
    [Required][ValidateObject] IReadOnlyCollection<ITypedExpression<ValidationError>> ValidationErrorExpressions { get; }
}
