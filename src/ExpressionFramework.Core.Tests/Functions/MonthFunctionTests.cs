namespace ExpressionFramework.Core.Tests.Functions;

public class MonthFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new MonthFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<MonthFunctionBuilder>();
        var monthFunctionBuilder = (MonthFunctionBuilder)actual;
        monthFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
