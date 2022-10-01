namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposableEvaluatable : IEvaluatable
{
    bool StartGroup { get; }
    bool EndGroup { get; }
    Combination Combination { get; }

    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IOperator Operator { get; }
    [Required]
    IExpression RightExpression { get; }
}
