namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class ChainedExpressionBuilder
{
    public ChainedExpressionBuilder Chain(params IExpressionBuilder[] expressions)
        => AddExpressions(expressions);

    public ChainedExpressionBuilder AddExpressions(params IExpressionBuilder[] expressions)
        => this.With(x => x.Expressions.AddRange(expressions));
}
