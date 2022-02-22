namespace ExpressionFramework.Core.Tests.Functions;

public class SumFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new SumFunction(functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<SumFunctionBuilder>();
        var sumFunctionBuilder = (SumFunctionBuilder)actual;
        sumFunctionBuilder.InnerFunction.Should().NotBeNull();
    }
}
