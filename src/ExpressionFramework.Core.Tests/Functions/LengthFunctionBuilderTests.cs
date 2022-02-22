namespace ExpressionFramework.Core.Tests.Functions;

public class LengthFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new LengthFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LengthFunction>();
        var lengthFunction = (LengthFunction)actual;
        if (functionFilled)
        {
            lengthFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            lengthFunction.InnerFunction.Should().BeNull();
        }
    }
}
