namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposableEvaluatable : IEvaluatable
{
    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IOperator Operator { get; }
    [Required]
    IExpression RightExpression { get; }

    Combination? Combination { get; }
    bool? StartGroup { get; }
    bool? EndGroup { get; }
}
