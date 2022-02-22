namespace ExpressionFramework.Core.Tests.Functions;

public class UpperFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new UpperFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<UpperFunction>();
        var upperFunction = (UpperFunction)actual;
        if (functionFilled)
        {
            upperFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            upperFunction.InnerFunction.Should().BeNull();
        }
    }
}
