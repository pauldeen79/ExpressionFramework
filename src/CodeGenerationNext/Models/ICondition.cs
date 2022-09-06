namespace CodeGenerationNext.Models;

public interface ICondition
{
    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IOperator Operator { get; }
    [Required]
    IExpression RightExpression { get; }
    bool StartGroup { get; }
    bool EndGroup { get; }
    Combination Combination { get; }
}
