namespace ExpressionFramework.Core.Extensions;

public static class EvaluatableExtensions
{
    public static Result<bool> Evaluate(this IEvaluatable instance)
        => instance.Evaluate(null);
}
