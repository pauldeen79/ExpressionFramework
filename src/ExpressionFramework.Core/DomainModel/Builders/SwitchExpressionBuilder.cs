namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class SwitchExpressionBuilder
{
    public SwitchExpressionBuilder Case(CaseBuilder caseBuilder)
        => AddCases(caseBuilder);

    public SwitchExpressionBuilder AddCases(params CaseBuilder[] caseBuilder)
        => this.With(x => x.Cases.AddRange(caseBuilder));

    public SwitchExpressionBuilder Default(IExpressionBuilder defaultExpression)
        => WithDefaultExpression(defaultExpression);

    public SwitchExpressionBuilder WithDefaultExpression(IExpressionBuilder defaultExpression)
        => this.With(x => x.DefaultExpression = defaultExpression);
}
