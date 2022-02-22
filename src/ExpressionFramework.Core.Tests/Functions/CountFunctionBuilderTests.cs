namespace ExpressionFramework.Core.Tests.Functions;

public class CountFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var expressionBuilderMock = TestFixtures.CreateExpressionBuilderMock();
        var sut = new CountFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object)
            .WithExpression(expressionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<CountFunction>();
        var countFunction = (CountFunction)actual;
        countFunction.Expression.Should().NotBeNull();
        if (functionFilled)
        {
            countFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            countFunction.InnerFunction.Should().BeNull();
        }
    }
}
