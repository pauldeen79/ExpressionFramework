namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    string? ErrorMessage { get; }
    [Required]
    IReadOnlyCollection<ValidationError> ValidationErrors { get; }
}
