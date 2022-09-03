namespace CodeGenerationNext.Models.Expressions;

public interface ISwitchExpression : IExpression
{
    IReadOnlyCollection<ICase> Cases { get; }
    IExpression DefaultExpression { get; }
}
