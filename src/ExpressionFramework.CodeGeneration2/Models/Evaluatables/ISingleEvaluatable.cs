namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface ISingleEvaluatable : IEvaluatable
{
    [Required][ValidateObject] IExpression LeftExpression { get; }
    [Required][ValidateObject] IOperator Operator { get; }
    [Required][ValidateObject] IExpression RightExpression { get; }
}
