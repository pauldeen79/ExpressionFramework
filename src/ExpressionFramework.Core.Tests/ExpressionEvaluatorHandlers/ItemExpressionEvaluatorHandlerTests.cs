namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ItemExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_False_When_Expression_Is_Not_A_ItemExpression()
    {
        // Arrange
        var sut = new ItemExpressionEvaluatorHandler();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_True_When_Expression_Is_A_ItemExpression()
    {
        // Arrange
        var sut = new ItemExpressionEvaluatorHandler();
        var expressionMock = new Mock<IItemExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(12345, null, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
