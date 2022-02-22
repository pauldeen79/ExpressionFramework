namespace ExpressionFramework.Core.Tests.Functions;

public class LowerFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new LowerFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LowerFunctionBuilder>();
        var lowerFunctionBuilder = (LowerFunctionBuilder)actual;
        lowerFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
