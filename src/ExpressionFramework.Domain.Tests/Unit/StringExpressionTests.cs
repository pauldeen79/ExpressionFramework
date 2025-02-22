namespace ExpressionFramework.Domain.Tests.Unit;

public class StringExpressionTests
{
    [Fact]
    public void GetStringEdgeDescriptor_Throws_On_Null_Type()
    {
        // Act & Assert
        Action a = () => _ = StringExpression.GetStringEdgeDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void GetStringTrimDescriptor_Throws_On_Null_Type()
    {
        // Act & Assert
        Action a = () => _ = StringExpression.GetStringTrimDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }
}
