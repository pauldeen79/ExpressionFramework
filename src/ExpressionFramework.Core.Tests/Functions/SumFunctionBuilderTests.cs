namespace ExpressionFramework.Core.Tests.Functions;

public class SumFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new SumFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<SumFunction>();
        var sumFunction = (SumFunction)actual;
        sumFunction.InnerFunction.Should().NotBeNull();
    }
}
