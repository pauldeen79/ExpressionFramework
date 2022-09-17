namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface ISingleEvaluatable : IEvaluatable
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
