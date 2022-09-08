namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class SwitchExpressionHandlerTests
{
    [Fact]
    public async Task Evaluate_Returns_NotSupported_When_Expression_Is_Not_A_SwitchExpression()
    {
        // Arrange
        var evaluationProviderMock = new Mock<IConditionEvaluatorProvider>();
        var sut = new SwitchExpressionHandler(evaluationProviderMock.Object);
        var expression = new EmptyExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public async Task Evaluate_Returns_Error_When_ConditionEvaluation_Fails()
    {
        // Arrange
        var evaluationProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        evaluationProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>()))
                              .Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<Condition>>()))
                              .ReturnsAsync(Result<bool>.Error("Kaboom"));
        var sut = new SwitchExpressionHandler(evaluationProviderMock.Object);
        var expression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder())
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public async Task Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var evaluationProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        evaluationProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>()))
                              .Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<Condition>>()))
                              .ReturnsAsync(Result<bool>.Success(true));
        var sut = new SwitchExpressionHandler(evaluationProviderMock.Object);
        var expression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder())
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
    public async Task Evaluate_Returns_Default_When_No_Cases_Are_Present()
    {
        // Arrange
        var evaluationProviderMock = new Mock<IConditionEvaluatorProvider>();
        var sut = new SwitchExpressionHandler(evaluationProviderMock.Object);
        var expression = new SwitchExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<EmptyExpression>()))
                               .ReturnsAsync(Result<object?>.Success(null));

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }
}
