namespace CodeGenerationNext.Models.Expressions;

public interface IConditionalExpression : IExpression
{
    [Required]
    IReadOnlyCollection<ICondition> Conditions { get; }
    [Required]
    IExpression ResultExpression { get; }
    [Required]
    IExpression DefaultExpression { get; }
}
