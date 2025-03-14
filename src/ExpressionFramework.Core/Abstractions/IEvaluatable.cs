namespace ExpressionFramework.Core.Abstractions;

public partial interface IEvaluatable
{
    Result<bool> Evaluate(object? context);
}
