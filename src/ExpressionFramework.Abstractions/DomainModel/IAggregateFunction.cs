namespace ExpressionFramework.Abstractions.DomainModel;

public interface IAggregateFunction
{
    IAggregateFunctionBuilder ToBuilder();
}
