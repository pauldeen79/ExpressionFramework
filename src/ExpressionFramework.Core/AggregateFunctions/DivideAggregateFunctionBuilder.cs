namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class DivideAggregateFunctionBuilder : IAggregateFunctionBuilder
{
    public IAggregateFunction Build()
        => new DivideAggregateFunction();
}
