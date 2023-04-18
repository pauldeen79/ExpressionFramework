namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISubstringExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression IndexExpression { get; }
    [Required]
    IExpression LengthExpression { get; }
}
