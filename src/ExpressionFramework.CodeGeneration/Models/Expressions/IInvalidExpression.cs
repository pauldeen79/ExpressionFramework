namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns an invalid result")]
public interface IInvalidExpression : IExpression
{
    [Required(AllowEmptyStrings = true)][ValidateObject][Description("Error message to use (may be empty)")] ITypedExpression<string> ErrorMessageExpression { get; }
    [Required][ValidateObject][Description("Optional validation errors to use")] IReadOnlyCollection <ITypedExpression<ValidationError>> ValidationErrorExpressions { get; }
}
