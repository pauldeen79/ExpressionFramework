namespace ExpressionFramework.Core.Tests.Functions;

public class MonthFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Full_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new MonthFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<MonthFunction>();
        var monthFunction = (MonthFunction)actual;
        if (functionFilled)
        {
            monthFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            monthFunction.InnerFunction.Should().BeNull();
        }
    }
}
