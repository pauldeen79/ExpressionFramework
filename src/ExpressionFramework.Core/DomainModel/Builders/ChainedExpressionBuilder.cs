namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class ChainedExpressionBuilder
{
    public ChainedExpressionBuilder Chain(params IExpressionBuilder[] expressions)
        => AddExpressions(expressions);
}
