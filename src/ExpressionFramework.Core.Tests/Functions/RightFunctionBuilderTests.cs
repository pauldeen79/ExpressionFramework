namespace ExpressionFramework.Core.Tests.Functions;

public class RightFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new RightFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<RightFunction>();
        var rightFunction = (RightFunction)actual;
        rightFunction.InnerFunction.Should().NotBeNull();
    }
}
