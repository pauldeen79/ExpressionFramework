namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class PlusAggregateFunctionBuilder : IAggregateFunctionBuilder
{
    public IAggregateFunction Build()
        => new PlusAggregateFunction();
}
