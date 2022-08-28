namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class MinusAggregateFunctionBuilder : IAggregateFunctionBuilder
{
    public IAggregateFunction Build()
        => new MinusAggregateFunction();
}
