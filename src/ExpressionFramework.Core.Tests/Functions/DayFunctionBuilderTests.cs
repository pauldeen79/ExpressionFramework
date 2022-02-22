namespace ExpressionFramework.Core.Tests.Functions;

public class DayFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new DayFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<DayFunction>();
        var dayFunction = (DayFunction)actual;
        if (functionFilled)
        {
            dayFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            dayFunction.InnerFunction.Should().BeNull();
        }
    }
}
