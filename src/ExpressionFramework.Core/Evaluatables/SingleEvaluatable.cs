namespace ExpressionFramework.Core.Evaluatables;

public class SingleEvaluatable : IEvaluatable
{
    public object? LeftValue { get; }
    public IOperator Operator { get; }
    public object? RightValue { get; }
    public StringComparison StringComparison { get; }

    public SingleEvaluatable(object? leftValue, IOperator @operator, object? rightValue, StringComparison stringComparison)
    {
        LeftValue = leftValue;
        Operator = @operator;
        RightValue = rightValue;
        StringComparison = stringComparison;
    }

    public Result<bool> Evaluate(object? context)
        => Operator.Evaluate(LeftValue, RightValue, StringComparison);
}
