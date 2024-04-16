namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IConstantEvaluatable : IEvaluatable
{
    bool Value { get; }
}
