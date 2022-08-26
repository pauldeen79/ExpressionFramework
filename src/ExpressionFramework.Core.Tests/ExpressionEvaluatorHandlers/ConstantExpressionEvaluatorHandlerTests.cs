namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ConstantExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_False_When_Expression_Is_Not_A_ConstantExpression()
    {
        // Arrange
        var sut = new ConstantExpressionEvaluatorHandler();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_True_When_Expression_Is_A_ConstantExpression()
    {
        // Arrange
        var sut = new ConstantExpressionEvaluatorHandler();
        var expressionMock = new Mock<IConstantExpression>();
        expressionMock.SetupGet(x => x.Value).Returns(12345);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
