namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class DelegateExpressionBuilder
{
    public DelegateExpressionBuilder WithValueDelegate(Func<object?, IExpression, IExpressionEvaluator, object?> valueDelegate)
        => this.With(x => x.ValueDelegate = valueDelegate);
}
