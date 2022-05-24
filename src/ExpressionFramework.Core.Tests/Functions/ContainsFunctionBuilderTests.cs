namespace ExpressionFramework.Core.Tests.Functions;

public class ContainsFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var expressionBuilderMock = TestFixtures.CreateExpressionBuilderMock();
        var sut = new ContainsFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object)
            .WithObjectToContain("4")
            .WithExpression(expressionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<ContainsFunction>();
        var containsFunction = (ContainsFunction)actual;
        containsFunction.Expression.Should().NotBeNull();
        containsFunction.ObjectToContain.Should().Be("4");
        if (functionFilled)
        {
            containsFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            containsFunction.InnerFunction.Should().BeNull();
        }
    }
}
