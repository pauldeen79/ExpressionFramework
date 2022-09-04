namespace OperatorFramework.Domain.Tests.Support;

public record UnknownOperator : Operator
{
    public override OperatorBuilder ToBuilder()
        => new UnknownOperatorBuilder();
}

public class UnknownOperatorBuilder : OperatorBuilder<UnknownOperatorBuilder, UnknownOperator>
{
    public override UnknownOperator BuildTyped()
        => new();
}
