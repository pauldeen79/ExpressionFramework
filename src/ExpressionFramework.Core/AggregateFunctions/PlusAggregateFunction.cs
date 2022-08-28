namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class PlusAggregateFunction : IAggregateFunction
{
    public IAggregateFunctionBuilder ToBuilder()
        => new PlusAggregateFunctionBuilder();
}
