namespace ExpressionFramework.Domain.Tests.Support;

public record UnknownExpression : Expression
{
    public override ExpressionBuilder ToBuilder()
        => new UnknownExpressionBuilder();
}

public class UnknownExpressionBuilder : ExpressionBuilder<UnknownExpressionBuilder, UnknownExpression>
{
    public override UnknownExpression BuildTyped()
        => new();
}
