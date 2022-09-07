namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class ChainedExpressionHandlerTests
{
    [Fact]
    public async Task Evaluate_Returns_NotSupported_When_Expression_Is_Not_A_ChainedExpression()
    {
        // Arrange
        var sut = new ChainedExpressionHandler();
        var expression = new EmptyExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public async Task Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var sut = new ChainedExpressionHandler();
        var expression = new ChainedExpressionBuilder()
            .AddExpressions(new EmptyExpressionBuilder())
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<Expression>()))
                               .ReturnsAsync(Result<object?>.Error("Kaboom"));

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public async Task Evaluate_Returns_Invalid_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var sut = new ChainedExpressionHandler();
        var expression = new ChainedExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("No expressions found");
    }
}
