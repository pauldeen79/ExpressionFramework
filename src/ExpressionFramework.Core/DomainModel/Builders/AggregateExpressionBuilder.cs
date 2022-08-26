namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class AggregateExpressionBuilder
{
    public AggregateExpressionBuilder WithAggregateFunction(IAggregateFunctionBuilder aggregateFunction)
        => this.With(x => x.AggregateFunction = aggregateFunction);
}
