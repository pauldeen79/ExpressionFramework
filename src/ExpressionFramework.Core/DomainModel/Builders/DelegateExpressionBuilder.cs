namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class DelegateExpressionBuilder
{
    public DelegateExpressionBuilder(Func<object?, object?, IExpression, IExpressionEvaluator, object?> sourceValueDelegate)
        : this(new DelegateExpression(sourceValueDelegate, null, null))
    {
    }

    public DelegateExpressionBuilder WithValueDelegate(Func<object?, object?, IExpression, IExpressionEvaluator, object?> valueDelegate)
        => this.With(x => x.ValueDelegate = valueDelegate);
}
