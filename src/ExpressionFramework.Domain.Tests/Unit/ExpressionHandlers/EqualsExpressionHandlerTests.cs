namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class EqualsExpressionHandlerTests
{
    [Fact]
    public async Task Handle_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var sut = new EqualsExpressionHandler();
        var evaluatorMock = new Mock<IExpressionEvaluator>();
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ConstantExpression("2");
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), firstExpression)).ReturnsAsync(Result<object?>.Error("Kaboom"));
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = await sut.Handle(null, expression, evaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public async Task Handle_Returns_Error_When_Evaluation_Of_SecondExpression_Fails()
    {
        // Arrange
        var sut = new EqualsExpressionHandler();
        var evaluatorMock = new Mock<IExpressionEvaluator>();
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ConstantExpression("2");
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<Expression>()))
                     .Returns<object?, Expression>((ctx, exp) => Task.FromResult(exp == firstExpression
                        ? Result<object?>.Success("Success value")
                        : Result<object?>.Error("Kaboom")));
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = await sut.Handle(null, expression, evaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }
}
