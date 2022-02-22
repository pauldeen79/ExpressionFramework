namespace ExpressionFramework.Core.Tests.Functions;

public class RightFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new RightFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<RightFunction>();
        var rightFunction = (RightFunction)actual;
        if (functionFilled)
        {
            rightFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            rightFunction.InnerFunction.Should().BeNull();
        }
    }
}
