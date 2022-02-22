namespace ExpressionFramework.Core.Tests.Functions;

public class LeftFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new LeftFunction(10, functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LeftFunctionBuilder>();
        var leftFunctionBuilder = (LeftFunctionBuilder)actual;
        leftFunctionBuilder.InnerFunction.Should().NotBeNull();
        leftFunctionBuilder.Length.Should().Be(10);
    }
}
