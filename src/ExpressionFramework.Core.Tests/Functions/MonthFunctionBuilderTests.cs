namespace ExpressionFramework.Core.Tests.Functions;

public class MonthFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new MonthFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<MonthFunction>();
        var monthFunction = (MonthFunction)actual;
        monthFunction.InnerFunction.Should().NotBeNull();
    }
}
