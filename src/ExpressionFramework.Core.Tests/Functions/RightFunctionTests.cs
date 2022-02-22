namespace ExpressionFramework.Core.Tests.Functions;

public class RightFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new RightFunction(10, functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<RightFunctionBuilder>();
        var leftFunctionBuilder = (RightFunctionBuilder)actual;
        leftFunctionBuilder.InnerFunction.Should().NotBeNull();
        leftFunctionBuilder.Length.Should().Be(10);
    }
}
