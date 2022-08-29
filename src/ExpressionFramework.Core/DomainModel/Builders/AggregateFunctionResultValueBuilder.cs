namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class AggregateFunctionResultValueBuilder
{
    public AggregateFunctionResultValueBuilder(object? value) : this()
        => Value = value;
}
