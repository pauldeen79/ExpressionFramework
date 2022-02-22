namespace ExpressionFramework.Core.Tests.Functions;

public class UpperFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new UpperFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<UpperFunction>();
        var upperFunction = (UpperFunction)actual;
        upperFunction.InnerFunction.Should().NotBeNull();
    }
}
