namespace ExpressionFramework.Core.AggregateFunctions;

[ExcludeFromCodeCoverage]
public class DivideAggregateFunction : IAggregateFunction
{
    public IAggregateFunctionBuilder ToBuilder()
        => new DivideAggregateFunctionBuilder();
}
