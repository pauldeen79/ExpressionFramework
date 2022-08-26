namespace ExpressionFramework.Abstractions.DomainModel;

public interface IAggregateFunctionResultValue
{
    object? Value { get; }
    bool Continue { get; }
}
