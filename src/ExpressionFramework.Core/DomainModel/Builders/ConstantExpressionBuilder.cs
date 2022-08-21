namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class ConstantExpressionBuilder
{
    public ConstantExpressionBuilder WithValue(object? value)
        => this.With(x => x.Value = value);

    public ConstantExpressionBuilder(object? sourceValue) : this(new ConstantExpression(sourceValue, null, null))
    {
    }
}
