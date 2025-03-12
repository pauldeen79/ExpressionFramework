namespace ExpressionFramework.Core.Abstractions;

public interface IEvaluatable
{
    Result<bool> Evaluate(object? context);
}
