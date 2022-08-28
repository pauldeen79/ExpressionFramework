namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
internal class EmptyAggregateFunction : IAggregateFunction
{
    public IAggregateFunctionBuilder ToBuilder()
        => new EmptyAggregateFunctionBuilder();
}
