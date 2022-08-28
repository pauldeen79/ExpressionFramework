namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorHandlers;

public class SwitchEpressionEvaluationHandlerTests
{
    [Fact]
    public void Handle_Returns_NotSupported_When_Expression_Is_Not_A_SwitchExpression()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var sut = new SwitchEpressionEvaluationHandler(conditionEvaluatorProviderMock.Object);
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_Correct_Result_When_Expression_Is_A_SwitchExpression_And_Case_Condition_Is_Satisfied()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.Success(true));
        var sut = new SwitchEpressionEvaluationHandler(conditionEvaluatorProviderMock.Object);
        const string tempResult = "yes";
        var switchExpression = new SwitchExpressionBuilder()
            .Case
            (
                new CaseBuilder()
                    .When(new ConditionBuilder()
                        .WithLeftExpression(new ConstantExpressionBuilder(true))
                        .WithOperator(Operator.Equal)
                        .WithRightExpression(new ConstantExpressionBuilder(true)))
                    .Then(new ConstantExpressionBuilder(tempResult))
            )
            .Default
            (
                new ConstantExpressionBuilder("no")
            )
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression) => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, switchExpression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Handle_Returns_Correct_Result_When_Expression_Is_A_SwitchExpression_And_Case_Condition_Is_Not_Satisfied()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>()))
                                      .Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.Success(false));
        var sut = new SwitchEpressionEvaluationHandler(conditionEvaluatorProviderMock.Object);
        const string tempResult = "yes";
        var switchExpression = new SwitchExpressionBuilder()
            .AddCases
            (
                new CaseBuilder()
                    .AddConditions(new ConditionBuilder()
                        .WithLeftExpression(new ConstantExpressionBuilder(true))
                        .WithOperator(Operator.Equal)
                        .WithRightExpression(new ConstantExpressionBuilder(false)))
                    .WithExpression(new ConstantExpressionBuilder("no"))
            )
            .WithDefaultExpression
            (
                new ConstantExpressionBuilder(tempResult)
            )
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression) => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, switchExpression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Handle_Returns_Correct_Result_When_ConditionEvaluation_Fails()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.Error("Something went wrong, please check"));
        var sut = new SwitchEpressionEvaluationHandler(conditionEvaluatorProviderMock.Object);
        var switchExpression = new SwitchExpressionBuilder()
            .Case
            (
                new CaseBuilder()
                    .When(new ConditionBuilder()
                        .WithLeftExpression(new ConstantExpressionBuilder(true))
                        .WithOperator(Operator.Equal)
                        .WithRightExpression(new ConstantExpressionBuilder(true)))
                    .Then(new ConstantExpressionBuilder("yes"))
            )
            .Default
            (
                new ConstantExpressionBuilder("no")
            )
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression) => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, switchExpression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Something went wrong, please check");
    }
}
