namespace ExpressionFramework.Core.Tests.Functions;

public class UpperFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new UpperFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<UpperFunctionBuilder>();
        var upperFunctionBuilder = (UpperFunctionBuilder)actual;
        upperFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
