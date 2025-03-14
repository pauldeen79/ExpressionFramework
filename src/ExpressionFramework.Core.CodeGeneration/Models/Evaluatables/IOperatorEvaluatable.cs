namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IOperatorEvaluatable : IEvaluatableBase
{
    object? LeftValue { get; }
    [Required][ValidateObject] Abstractions.IOperator Operator { get; }
    object? RightValue { get; }
    StringComparison StringComparison { get; }
}
