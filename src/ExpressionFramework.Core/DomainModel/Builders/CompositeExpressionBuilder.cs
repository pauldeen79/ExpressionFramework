namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class CompositeExpressionBuilder
{
    public CompositeExpressionBuilder WithCompositeFunction(ICompositeFunctionBuilder compositeFunction)
        => this.With(x => x.CompositeFunction = compositeFunction);
}
