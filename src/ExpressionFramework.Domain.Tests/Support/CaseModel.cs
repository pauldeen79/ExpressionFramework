namespace ExpressionFramework.Domain.Tests.Support;

public class CaseModel
{
    public string Expression { get; set; } = "";
    public string ConditionLeftExpression { get; set; } = "";
    public string ConditionOperator { get; set; } = "Equals";
    public string ConditionRightExpression { get; set; } = "";

    public Case ToCase() => ToCaseBuilder().Build();

    public CaseBuilder ToCaseBuilder() =>
        new CaseBuilder()
            .AddConditions(new ConditionBuilder(new ConditionModel
            {
                LeftExpression = ConditionLeftExpression,
                Operator = ConditionOperator,
                RightExpression = ConditionRightExpression
            }.ToCondition()))
            .WithExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(Expression)));
}
