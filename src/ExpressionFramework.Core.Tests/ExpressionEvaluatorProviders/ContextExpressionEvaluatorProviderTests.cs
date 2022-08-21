namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ContextExpressionEvaluatorProviderTests
{
    [Fact]
    public void TryEvaluate_Returns_False_When_Expression_Is_Not_A_ContextExpression()
    {
        // Arrange
        var sut = new ContextExpressionEvaluatorProvider();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_ContextExpression()
    {
        // Arrange
        var sut = new ContextExpressionEvaluatorProvider();
        var expressionMock = new Mock<IContextExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, 12345, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(12345);
    }
}
