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
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(LeftExpression)))
            .WithOperator(OperatorBuilderFactory.Create(OperatorExpressionParser.Parse(Operator)))
            .WithRightExpression(new ConstantExpressionBuilder().WithValue(StringExpression.Parse(RightExpression)))
            .WithStartGroup(StartGroup)
            .WithEndGroup(EndGroup)
            .WithCombination(Combination)
            .BuildTyped();
}
