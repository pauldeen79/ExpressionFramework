namespace CodeGenerationNext.Models.Expressions;

public interface IAccumulatorExpression : IExpression
{
    bool IsFirstItem { get; }
    object? Value { get; }
    object? Context { get; }
    [Required]
    IExpression Expression { get; }
}
