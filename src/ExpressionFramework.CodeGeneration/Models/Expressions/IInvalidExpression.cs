namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    [Required]
    IExpression ErrorMessageExpression { get; }
    [Required]
    IReadOnlyCollection<ValidationError> ValidationErrors { get; }
}
