namespace ExpressionFramework.Core.Tests.Functions;

public class LengthFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new LengthFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LengthFunctionBuilder>();
        var lengthFunctionBuilder = (LengthFunctionBuilder)actual;
        lengthFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
