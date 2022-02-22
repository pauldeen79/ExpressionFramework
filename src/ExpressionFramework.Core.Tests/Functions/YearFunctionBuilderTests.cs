namespace ExpressionFramework.Core.Tests.Functions;

public class YearFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new YearFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<YearFunction>();
        var yearFunction = (YearFunction)actual;
        yearFunction.InnerFunction.Should().NotBeNull();
    }
}
