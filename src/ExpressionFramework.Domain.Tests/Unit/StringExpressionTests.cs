namespace ExpressionFramework.Domain.Tests.Unit;

public class StringExpressionTests
{
    [Fact]
    public void GetStringEdgeDescriptor_Throws_On_Null_Type()
    {
        // Act & Assert
        this.Invoking(_ => StringExpression.GetStringEdgeDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void GetStringTrimDescriptor_Throws_On_Null_Type()
    {
        // Act & Assert
        this.Invoking(_ => StringExpression.GetStringTrimDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }
}
