namespace ExpressionFramework.Core.Tests.Functions;

public class LowerFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new LowerFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LowerFunction>();
        var lowerFunction = (LowerFunction)actual;
        if (functionFilled)
        {
            lowerFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            lowerFunction.InnerFunction.Should().BeNull();
        }
    }
}
