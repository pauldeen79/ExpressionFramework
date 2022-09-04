namespace ExpressionFramework.Domain;

public interface IConditionEvaluator
{
    Task<Result<bool>> Evaluate(object? context, IEnumerable<Condition> conditions);
}
