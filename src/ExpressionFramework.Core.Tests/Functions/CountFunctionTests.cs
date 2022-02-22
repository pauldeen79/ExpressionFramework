namespace ExpressionFramework.Core.Tests.Functions;

public class CountFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var expressionMock = TestFixtures.CreateExpressionMock();
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new CountFunction(expressionMock.Object, functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<CountFunctionBuilder>();
        var countFunctionBuilder = (CountFunctionBuilder)actual;
        countFunctionBuilder.InnerFunction.Should().NotBeNull();
        countFunctionBuilder.Expression.Should().NotBeNull();
    }
}
