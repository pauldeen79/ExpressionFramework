namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITernaryExpression : IExpression
{
    [Required]
    IReadOnlyCollection<ICondition> Conditions { get; }
}
