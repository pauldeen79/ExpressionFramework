namespace CodeGenerationNext.Models;

public interface ICase
{
    IReadOnlyCollection<ICondition> Conditions { get; }
    IExpression Expression { get; }
}
