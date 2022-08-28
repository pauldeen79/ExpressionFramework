namespace ExpressionFramework.Abstractions;

public interface IConditionEvaluator
{
    Result<bool> Evaluate(object? context, IEnumerable<ICondition> conditions);
}
