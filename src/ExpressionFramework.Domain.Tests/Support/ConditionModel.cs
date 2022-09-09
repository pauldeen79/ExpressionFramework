namespace ExpressionFramework.Domain.Tests.Support;

public class ConditionModel 
{
    public string LeftExpression { get; set; } = "";
    public string Operator { get; set; } = "Equals";
    public string RightExpression { get; set; } = "";
    public bool StartGroup { get; set; }
    public bool EndGroup { get; set; }
    public Combination Combination { get; set; }
    public Condition ToCondition() =>
        new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(LeftExpression)))
            .WithOperator(OperatorBuilderFactory.Create(OperatorExpression.Evaluate(Operator)))
            .WithRightExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(RightExpression)))
            .WithStartGroup(StartGroup)
            .WithEndGroup(EndGroup)
            .WithCombination(Combination)
            .Build();
}
