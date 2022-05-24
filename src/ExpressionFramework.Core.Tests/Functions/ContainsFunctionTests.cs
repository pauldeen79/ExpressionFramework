namespace ExpressionFramework.Core.Tests.Functions;

public class ContainsFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var expressionMock = TestFixtures.CreateExpressionMock();
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new ContainsFunction(expressionMock.Object, "53", functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<ContainsFunctionBuilder>();
        var containsFunctionBuilder = (ContainsFunctionBuilder)actual;
        if (functionFilled)
        {
            containsFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            containsFunctionBuilder.InnerFunction.Should().BeNull();
        }
        containsFunctionBuilder.ObjectToContain.Should().Be("53");
        containsFunctionBuilder.Expression.Should().NotBeNull();
    }
}
