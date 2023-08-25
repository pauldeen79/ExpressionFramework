namespace ExpressionFramework.Domain.Tests.Unit;

public class EnumerableExpressionTests
{
    [Fact]
    public void GetDescriptor_Throws_On_Null_Type()
    {
        this.Invoking(_ => EnumerableExpression.GetDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, typeof(object)))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }
}
