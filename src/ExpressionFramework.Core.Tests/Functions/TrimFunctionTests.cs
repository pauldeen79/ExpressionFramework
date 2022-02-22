namespace ExpressionFramework.Core.Tests.Functions;

public class TrimFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new TrimFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<TrimFunctionBuilder>();
        var trimFunctionBuilder = (TrimFunctionBuilder)actual;
        trimFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
