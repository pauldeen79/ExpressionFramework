namespace ExpressionFramework.Core.Tests.Functions;

public class YearFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new YearFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<YearFunctionBuilder>();
        var yearFunctionBuilder = (YearFunctionBuilder)actual;
        if (functionFilled)
        {
            yearFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            yearFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
