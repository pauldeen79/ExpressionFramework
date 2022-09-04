namespace ExpressionFramework.Domain.Tests.Support;

public record UnknownOperator : Operator
{
}

public class UnknownOperatorBuilder : OperatorBuilder<UnknownOperatorBuilder, UnknownOperator>
{
    public override UnknownOperator BuildTyped()
        => new();
}
