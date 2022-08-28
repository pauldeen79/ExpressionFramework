namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorHandlers;

public class ConditionalExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_NotSupported_When_Expression_Is_Not_A_ConditionalExpression()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var sut = new ConditionalExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object);
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_ResultExpression_When_Expression_Is_A_ConditionalExpression_And_Evaluation_Is_True()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.Success(true));
        var sut = new ConditionalExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object);
        var expressionMock = new Mock<IConditionalExpression>();
        expressionMock.SetupGet(x => x.ResultExpression)
                      .Returns(new ConstantExpressionBuilder(1).Build());
        expressionMock.SetupGet(x => x.DefaultExpression)
                      .Returns(new ConstantExpressionBuilder(2).Build());
        expressionMock.SetupGet(x => x.Conditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, IExpression>((context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Handle_Returns_ResultExpression_When_Expression_Is_A_ConditionalExpression_And_Evaluation_Is_False()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.Success(false));
        var sut = new ConditionalExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object);
        var expressionMock = new Mock<IConditionalExpression>();
        expressionMock.SetupGet(x => x.ResultExpression)
                      .Returns(new ConstantExpressionBuilder(1).Build());
        expressionMock.SetupGet(x => x.DefaultExpression)
                      .Returns(new ConstantExpressionBuilder(2).Build());
        expressionMock.SetupGet(x => x.Conditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, IExpression>((context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(2);
    }

    [Fact]
    public void Handle_Returns_ConditionEvaluation_Result_When_This_Is_Not_Successful()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns(Result<bool>.NotSupported("Kaboom"));
        var sut = new ConditionalExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object);
        var expressionMock = new Mock<IConditionalExpression>();
        expressionMock.SetupGet(x => x.ResultExpression)
                      .Returns(new ConstantExpressionBuilder(1).Build());
        expressionMock.SetupGet(x => x.DefaultExpression)
                      .Returns(new ConstantExpressionBuilder(2).Build());
        expressionMock.SetupGet(x => x.Conditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, IExpression>((context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
        actual.ErrorMessage.Should().Be("Kaboom");
    }
}
