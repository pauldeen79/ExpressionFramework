namespace ExpressionFramework.Domain.Tests.Support;

public class ConditionModel 
{
    public string LeftExpression { get; set; } = "a";
    public string Operator { get; set; } = "Equals";
    public string RightExpression { get; set; } = "b";
    public bool StartGroup { get; set; }
    public bool EndGroup { get; set; }
    public Combination Combination { get; set; }
    public Condition ToCondition() =>
        new(
            new ConstantExpression(StringExpression.Parse(LeftExpression)),
            OperatorExpression.Evaluate(Operator),
            new ConstantExpression(StringExpression.Parse(RightExpression)),
            StartGroup,
            EndGroup,
            Combination); 
}
