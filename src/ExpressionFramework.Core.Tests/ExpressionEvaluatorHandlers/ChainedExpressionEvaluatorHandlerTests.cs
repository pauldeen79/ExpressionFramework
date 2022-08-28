namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorHandlers;

public class ChainedExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_NotSupported_When_Expression_Is_Not_A_ChainedExpression()
    {
        // Arrange
        var sut = new ChainedExpressionEvaluatorHandler();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var sut = new ChainedExpressionEvaluatorHandler();
        var expressionMock = new Mock<IChainedExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new Mock<IExpression>().Object }));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns(Result<object?>.Error("Kaboom"));

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Handle_Returns_Invalid_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var sut = new ChainedExpressionEvaluatorHandler();
        var expressionMock = new Mock<IChainedExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("No expressions found");
    }
}
