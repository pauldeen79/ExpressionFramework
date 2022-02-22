namespace ExpressionFramework.Core.Tests.Functions;

public class YearFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new YearFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<YearFunction>();
        var yearFunction = (YearFunction)actual;
        if (functionFilled)
        {
            yearFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            yearFunction.InnerFunction.Should().BeNull();
        }
    }
}
