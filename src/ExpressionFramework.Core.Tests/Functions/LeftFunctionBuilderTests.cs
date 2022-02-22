namespace ExpressionFramework.Core.Tests.Functions;

public class LeftFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new LeftFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LeftFunction>();
        var leftFunction = (LeftFunction)actual;
        leftFunction.InnerFunction.Should().NotBeNull();
    }
}
