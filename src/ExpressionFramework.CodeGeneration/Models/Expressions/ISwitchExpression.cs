namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISwitchExpression : IExpression
{
    [Required]
    IReadOnlyCollection<ICase> Cases { get; }
    IExpression? DefaultExpression { get; }
}
