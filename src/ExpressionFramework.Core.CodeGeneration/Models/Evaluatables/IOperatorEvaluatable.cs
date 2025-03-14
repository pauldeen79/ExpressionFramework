namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IOperatorEvaluatable : IEvaluatableBase
{
    [Required][ValidateObject] object? LeftValue { get; }
    [Required][ValidateObject] Abstractions.IOperator Operator { get; }
    [Required][ValidateObject] object? RightValue { get; }
    StringComparison StringComparison { get; }
}
