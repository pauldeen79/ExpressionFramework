namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IRightExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    IExpression LengthExpression { get; }
}
