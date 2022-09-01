namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class DelegateExpressionBuilder
{
    public DelegateExpressionBuilder(Func<IDelegateExpressionRequest, IDelegateExpressionResponse> sourceValueDelegate)
        : this(new DelegateExpression(sourceValueDelegate, null))
    {
    }

    public DelegateExpressionBuilder WithValueDelegate(Func<IDelegateExpressionRequest, IDelegateExpressionResponse> valueDelegate)
        => this.With(x => x.ValueDelegate = valueDelegate);
}
