namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConcatenateExpression : IExpression
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
