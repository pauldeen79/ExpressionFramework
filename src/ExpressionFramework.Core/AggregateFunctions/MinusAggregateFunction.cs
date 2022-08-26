namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class MinusAggregateFunction : IAggregateFunction
{
    public IAggregateFunctionBuilder ToBuilder()
        => new MinusAggregateFunctionBuilder();
}
