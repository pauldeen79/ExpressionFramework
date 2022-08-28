namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class CaseBuilder
{
    public CaseBuilder When(ConditionBuilder conditionBuilder)
        => AddConditions(conditionBuilder);

    public CaseBuilder Then(IExpressionBuilder expression)
        => this.With(x => x.Expression = expression);

    public CaseBuilder WithExpression(IExpressionBuilder expression)
        => this.With(x => x.Expression = expression);
}
