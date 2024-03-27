namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposableEvaluatable : IEvaluatable
{
    [Required][ValidateObject] IExpression LeftExpression { get; }
    [Required][ValidateObject] IOperator Operator { get; }
    [Required][ValidateObject] IExpression RightExpression { get; }

    Combination? Combination { get; }
    bool? StartGroup { get; }
    bool? EndGroup { get; }
}
