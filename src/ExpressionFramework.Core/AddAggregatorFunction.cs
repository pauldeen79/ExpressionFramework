namespace ExpressionFramework.Core;

[FunctionName("AddAggregator")]
public class AddAggregatorFunction : IFunction
{
    private static readonly AddAggregator _addAggregator = new AddAggregator();

    public Result<object?> Evaluate(FunctionCallContext context)
        => Result.Success<object?>(_addAggregator);
}
