namespace ExpressionFramework.Core.Tests.Functions;

public class TrimFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new TrimFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<TrimFunction>();
        var trimFunction = (TrimFunction)actual;
        if (functionFilled)
        {
            trimFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            trimFunction.InnerFunction.Should().BeNull();
        }
    }
}
