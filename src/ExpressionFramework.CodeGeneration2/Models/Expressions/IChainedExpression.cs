namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IChainedExpression : IExpression
{
    [Required][ValidateObject] IReadOnlyCollection<IExpression> Expressions { get; }
}
