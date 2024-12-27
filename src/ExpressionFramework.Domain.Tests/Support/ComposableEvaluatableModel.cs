namespace ExpressionFramework.Domain.Tests.Support;

public class ComposableEvaluatableModel
{
    public string LeftExpression { get; set; } = "";
    public string Operator { get; set; } = "Equals";
    public string RightExpression { get; set; } = "";
    public bool StartGroup { get; set; }
    public bool EndGroup { get; set; }
    public Combination Combination { get; set; }
    public ComposableEvaluatable ToEvaluatable() =>
        new ComposableEvaluatableBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(LeftExpression)))
            .WithOperator(OperatorExpressionParser.Parse(Operator).ToBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue(StringExpressionParser.Parse(RightExpression)))
            .WithStartGroup(StartGroup)
            .WithEndGroup(EndGroup)
            .WithCombination(Combination)
            .BuildTyped();
}
