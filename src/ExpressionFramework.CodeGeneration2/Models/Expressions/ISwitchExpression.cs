namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISwitchExpression : IExpression
{
    [Required][ValidateObject] IReadOnlyCollection<ICase> Cases { get; }
    [ValidateObject] IExpression? DefaultExpression { get; }
}
