namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ConstantExpressionEvaluatorProviderTests
{
    [Fact]
    public void Evaluate_Returns_False_When_Expression_Is_Not_A_ConstantExpression()
    {
        // Arrange
        var sut = new ConstantExpressionEvaluatorProvider();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Evaluate_Returns_True_When_Expression_Is_A_ConstantExpression()
    {
        // Arrange
        var sut = new ConstantExpressionEvaluatorProvider();
        var expressionMock = new Mock<IConstantExpression>();
        expressionMock.SetupGet(x => x.Value).Returns(12345);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
