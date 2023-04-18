namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITrimEndExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IExpression Expression { get; }
    IExpression? TrimCharsExpression { get; }
}
