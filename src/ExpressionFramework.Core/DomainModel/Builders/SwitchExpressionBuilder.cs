namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class SwitchExpressionBuilder
{
    public SwitchExpressionBuilder Case(CaseBuilder caseBuilder)
        => AddCases(caseBuilder);

    public SwitchExpressionBuilder Default(IExpressionBuilder defaultExpression)
        => WithDefaultExpression(defaultExpression);

    public SwitchExpressionBuilder WithDefaultExpression(IExpressionBuilder defaultExpression)
        => this.With(x => x.DefaultExpression = defaultExpression);
}
