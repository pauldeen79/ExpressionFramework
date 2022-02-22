namespace ExpressionFramework.Core.Tests.Functions;

public class YearFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new YearFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<YearFunctionBuilder>();
        var yearFunctionBuilder = (YearFunctionBuilder)actual;
        yearFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
