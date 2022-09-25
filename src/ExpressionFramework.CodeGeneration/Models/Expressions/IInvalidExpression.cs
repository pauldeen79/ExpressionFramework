namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    [Required]
    string ErrorMessage { get; }
    [Required]
    IReadOnlyCollection<ValidationError> ValidationErrors { get; }
}
