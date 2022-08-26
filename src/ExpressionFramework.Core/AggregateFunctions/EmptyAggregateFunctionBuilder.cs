namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
internal class EmptyAggregateFunctionBuilder : IAggregateFunctionBuilder
{
    public IAggregateFunction Build()
        => new EmptyAggregateFunction();
}
