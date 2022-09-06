namespace CodeGenerationNext.Models;

public interface ICase
{
    [Required]
    IReadOnlyCollection<ICondition> Conditions { get; }
    [Required]
    IExpression Expression { get; }
}
