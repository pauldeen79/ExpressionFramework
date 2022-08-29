namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class AggregateExpressionBuilder
{
    public AggregateExpressionBuilder WithAggregateFunction(IAggregateFunctionBuilder aggregateFunction)
        => this.With(x => x.AggregateFunction = aggregateFunction);

    public AggregateExpressionBuilder Aggregate(params IExpressionBuilder[] expressions)
        => AddExpressions(expressions);

    public AggregateExpressionBuilder AddExpressions(params IExpressionBuilder[] expressions)
        => this.With(x => x.Expressions.AddRange(expressions));

    public AggregateExpressionBuilder AddExpressionConditions(params IConditionBuilder[] conditions)
        => this.With(x => x.ExpressionConditions.AddRange(conditions));
}
