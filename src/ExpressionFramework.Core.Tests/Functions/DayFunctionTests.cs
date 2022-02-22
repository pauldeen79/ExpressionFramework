namespace ExpressionFramework.Core.Tests.Functions;

public class DayFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new DayFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<DayFunctionBuilder>();
        var dayFunctionBuilder = (DayFunctionBuilder)actual;
        if (functionFilled)
        {
            dayFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            dayFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
