namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IConstantEvaluatable : IEvaluatableBase
{
    bool Value { get; }
}
