namespace ExpressionFramework.Domain.Tests.Support;

public record UnknownExpression : Expression
{
}

public class UnknownExpressionBuilder : ExpressionBuilder<UnknownExpressionBuilder, UnknownExpression>
{
    public override UnknownExpression BuildTyped()
        => new();
}
