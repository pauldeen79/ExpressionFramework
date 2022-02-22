namespace ExpressionFramework.Core.Tests.Functions;

public class TrimFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new TrimFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<TrimFunction>();
        var trimFunction = (TrimFunction)actual;
        trimFunction.InnerFunction.Should().NotBeNull();
    }
}
