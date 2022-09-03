namespace CodeGenerationNext.Models;

public interface ISwitchExpression : IExpression
{
    IReadOnlyCollection<ICase> Cases { get; }
    IExpression DefaultExpression { get; }
}
