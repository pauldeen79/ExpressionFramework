namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class MultiplyAggregateFunction : IAggregateFunction
{
    public IAggregateFunctionBuilder ToBuilder()
        => new MultiplyAggregateFunctionBuilder();
}
