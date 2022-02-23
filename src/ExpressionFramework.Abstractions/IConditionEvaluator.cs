namespace ExpressionFramework.Abstractions;

public interface IConditionEvaluator
{
    bool Evaluate(object? context, IEnumerable<ICondition> conditions);
}
