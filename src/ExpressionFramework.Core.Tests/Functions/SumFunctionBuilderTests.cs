namespace ExpressionFramework.Core.Tests.Functions;

public class SumFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new SumFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<SumFunction>();
        var sumFunction = (SumFunction)actual;
        if (functionFilled)
        {
            sumFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            sumFunction.InnerFunction.Should().BeNull();
        }
    }
}
