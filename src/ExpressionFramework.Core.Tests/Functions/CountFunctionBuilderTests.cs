namespace ExpressionFramework.Core.Tests.Functions;

public class CountFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var expressionBuilderMock = TestFixtures.CreateExpressionBuilderMock();
        var sut = new CountFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object)
            .WithExpression(expressionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<CountFunction>();
        var countFunction = (CountFunction)actual;
        countFunction.Expression.Should().NotBeNull();
        countFunction.InnerFunction.Should().NotBeNull();
    }
}
