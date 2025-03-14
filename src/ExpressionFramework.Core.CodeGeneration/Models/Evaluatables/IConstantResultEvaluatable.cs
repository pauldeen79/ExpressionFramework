namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IConstantResultEvaluatable : IEvaluatableBase
{
    Result<bool> Result { get; }
}
