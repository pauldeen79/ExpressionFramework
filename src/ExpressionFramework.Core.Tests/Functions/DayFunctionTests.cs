namespace ExpressionFramework.Core.Tests.Functions;

public class DayFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new DayFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<DayFunctionBuilder>();
        var dayFunctionBuilder = (DayFunctionBuilder)actual;
        dayFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
