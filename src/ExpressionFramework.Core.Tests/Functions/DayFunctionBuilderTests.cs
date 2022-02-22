namespace ExpressionFramework.Core.Tests.Functions;

public class DayFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new DayFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<DayFunction>();
        var dayFunction = (DayFunction)actual;
        dayFunction.InnerFunction.Should().NotBeNull();
    }
}
