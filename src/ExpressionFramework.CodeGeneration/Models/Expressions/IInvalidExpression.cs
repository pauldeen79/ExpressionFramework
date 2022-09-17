namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IInvalidExpression : IExpression
{
    string ErrorMessage { get; }
    IReadOnlyCollection<ValidationError> ValidationErrors { get; }
}
