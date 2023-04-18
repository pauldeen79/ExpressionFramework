namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConcatenateExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IReadOnlyCollection<IExpression> Expressions { get; }
}
