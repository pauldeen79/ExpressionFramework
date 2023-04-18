namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimStartExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
    IExpression? TrimCharsExpression { get; }
}
