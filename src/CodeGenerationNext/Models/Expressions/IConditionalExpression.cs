namespace CodeGenerationNext.Models.Expressions;

public interface IConditionalExpression : IExpression
{
    IReadOnlyCollection<ICondition> Conditions { get; }
    IExpression ResultExpression { get; }
    IExpression DefaultExpression { get; }
}
