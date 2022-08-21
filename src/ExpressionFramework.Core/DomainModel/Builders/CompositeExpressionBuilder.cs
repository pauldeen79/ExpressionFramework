namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class CompositeExpressionBuilder
{
    public CompositeExpressionBuilder WithCompositeFunction(ICompositeFunction compositeFunction)
        => this.With(x => x.CompositeFunction = compositeFunction);
}
