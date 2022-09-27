namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface ISingleEvaluatable : IEvaluatable
{
    [Required]
    IExpression LeftExpression { get; }
    [Required]
    IOperator Operator { get; }
    [Required]
    IExpression RightExpression { get; }
}
