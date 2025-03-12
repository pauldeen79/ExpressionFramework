namespace ExpressionFramework.Core.Evaluatables;

public class ComposableEvaluatable : IEvaluatable
{
    public object? Value1 { get; }
    public IOperator Operator { get; }
    public object? Value2 { get; }
    public StringComparison StringComparison { get; }
    public Combination? Combination { get; }
    public bool StartGroup { get; }
    public bool EndGroup { get; }

    public ComposableEvaluatable(object? value1, IOperator @operator, object? value2, StringComparison stringComparison, Combination? combination, bool startGroup, bool endGroup)
    {
        Value1 = value1;
        Operator = @operator;
        Value2 = value2;
        StringComparison = stringComparison;
        Combination = combination;
        StartGroup = startGroup;
        EndGroup = endGroup;
    }

    public Result<bool> Evaluate(object? context)
        => Operator.Evaluate(Value1, Value2, StringComparison);
}
