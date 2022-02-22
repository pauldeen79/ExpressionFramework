namespace ExpressionFramework.Core.Tests.Functions;

public class LeftFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new LeftFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LeftFunction>();
        var leftFunction = (LeftFunction)actual;
        if (functionFilled)
        {
            leftFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            leftFunction.InnerFunction.Should().BeNull();
        }
    }
}
