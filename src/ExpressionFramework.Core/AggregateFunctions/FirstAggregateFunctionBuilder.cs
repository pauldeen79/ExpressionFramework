namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class FirstAggregateFunctionBuilder : IAggregateFunctionBuilder
{
    public IAggregateFunction Build()
        => new FirstAggregateFunction();
}
