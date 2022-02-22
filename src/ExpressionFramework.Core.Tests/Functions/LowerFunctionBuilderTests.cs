namespace ExpressionFramework.Core.Tests.Functions;

public class LowerFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new LowerFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LowerFunction>();
        var lowerFunction = (LowerFunction)actual;
        lowerFunction.InnerFunction.Should().NotBeNull();
    }
}
