namespace ExpressionFramework.Core.Evaluatables;

public class ComposableEvaluatable : IEvaluatable
{
    public object? LeftValue { get; }
    public IOperator Operator { get; }
    public object? RightValue { get; }
    public StringComparison StringComparison { get; }
    public Combination? Combination { get; }
    public bool StartGroup { get; }
    public bool EndGroup { get; }

    public ComposableEvaluatable(object? leftValue, IOperator @operator, object? rightValue, StringComparison stringComparison, Combination? combination, bool startGroup, bool endGroup)
    {
        LeftValue = leftValue;
        Operator = @operator;
        RightValue = rightValue;
        StringComparison = stringComparison;
        Combination = combination;
        StartGroup = startGroup;
        EndGroup = endGroup;
    }

    public Result<bool> Evaluate(object? context)
        => Operator.Evaluate(LeftValue, RightValue, StringComparison);
}
