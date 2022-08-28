namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class AggregateFunctionResultValueBuilder
{
    public AggregateFunctionResultValueBuilder(object? value) : this()
        => Value = value;

    public AggregateFunctionResultValueBuilder Stop()
        => this.With(x => x.Continue = false);

    public AggregateFunctionResultValueBuilder WithContinue(bool @continue = true)
        => this.With(x => x.Continue = @continue);
}
