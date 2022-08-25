namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class EmptyExpressionEvaluatorProviderTests
{
    [Fact]
    public void Evaluate_Returns_False_When_Expression_Is_Not_A_EmptyExpression()
    {
        // Arrange
        var sut = new EmptyExpressionEvaluatorProvider();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Evaluate_Returns_True_When_Expression_Is_A_EmptyExpression()
    {
        // Arrange
        var sut = new EmptyExpressionEvaluatorProvider();
        var expressionMock = new Mock<IEmptyExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeNull();
    }
}
