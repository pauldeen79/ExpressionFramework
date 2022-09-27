namespace ExpressionFramework.CodeGeneration.Models.Evaluatables;

public interface IComposableEvaluatable : ISingleEvaluatable
{
    bool StartGroup { get; }
    bool EndGroup { get; }
    Combination Combination { get; }
}
