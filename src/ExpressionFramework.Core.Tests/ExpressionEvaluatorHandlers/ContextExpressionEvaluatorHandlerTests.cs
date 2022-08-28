namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ContextExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_False_When_Expression_Is_Not_A_ContextExpression()
    {
        // Arrange
        var sut = new ContextExpressionEvaluatorHandler();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_True_When_Expression_Is_A_ContextExpression()
    {
        // Arrange
        var sut = new ContextExpressionEvaluatorHandler();
        var expressionMock = new Mock<IContextExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(12345, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
