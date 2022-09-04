namespace CodeGenerationNext.Models.Expressions;

public interface ISwitchExpression : IExpression
{
    [Required]
    IReadOnlyCollection<ICase> Cases { get; }
    [Required]
    IExpression DefaultExpression { get; }
}
