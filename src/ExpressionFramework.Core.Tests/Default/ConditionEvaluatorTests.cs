namespace ExpressionFramework.Core.Tests.Default;

public class ConditionEvaluatorTests
{
    private readonly Mock<IExpressionEvaluator> _expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
    private ConditionEvaluator CreateSut() => new ConditionEvaluator(_expressionEvaluatorMock.Object);

    [Fact]
    public void IsItemValid_Throws_On_Unsupported_Operator()
    {
        // Arrange
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.Operator)
                     .Returns((Operator)int.MaxValue);
        conditionMock.SetupGet(x => x.LeftExpression)
                     .Returns(new EmptyExpressionBuilder().Build());
        conditionMock.SetupGet(x => x.RightExpression)
                     .Returns(new EmptyExpressionBuilder().Build());

        // Act
        CreateSut().Invoking(x => x.Evaluate(null, new[] { conditionMock.Object }))
                   .Should().ThrowExactly<ArgumentOutOfRangeException>()
                   .WithParameterName("condition")
                   .And.Message.Should().StartWith($"Unsupported operator: {int.MaxValue}");
    }

    [Theory]
    [InlineData("A", "A", Operator.Equal, true)]
    [InlineData("A", "a", Operator.Equal, true)]
    [InlineData("A", "b", Operator.Equal, false)]
    [InlineData(null, "b", Operator.Equal, false)]
    [InlineData("A", null, Operator.Equal, false)]
    [InlineData("", "", Operator.Equal, true)]
    [InlineData(null, null, Operator.Equal, true)]
    [InlineData(true, true, Operator.Equal, true)]
    [InlineData(true, false, Operator.Equal, false)]
    [InlineData(1, 1, Operator.Equal, true)]
    [InlineData(1, 2, Operator.Equal, false)]
    [InlineData(true, 1, Operator.Equal, false)]
    [InlineData(true, "", Operator.Equal, false)]
    [InlineData("A", "A", Operator.NotEqual, false)]
    [InlineData("A", "a", Operator.NotEqual, false)]
    [InlineData("A", "b", Operator.NotEqual, true)]
    [InlineData(null, "b", Operator.NotEqual, true)]
    [InlineData("A", null, Operator.NotEqual, true)]
    [InlineData(1, 2, Operator.Greater, false)]
    [InlineData(2, 2, Operator.Greater, false)]
    [InlineData(3, 2, Operator.Greater, true)]
    [InlineData(null, 2, Operator.Greater, false)]
    [InlineData(2, null, Operator.Greater, false)]
    [InlineData(1, 2, Operator.GreaterOrEqual, false)]
    [InlineData(2, 2, Operator.GreaterOrEqual, true)]
    [InlineData(3, 2, Operator.GreaterOrEqual, true)]
    [InlineData(null, 2, Operator.GreaterOrEqual, false)]
    [InlineData(1, null, Operator.GreaterOrEqual, false)]
    [InlineData(2, 1, Operator.Smaller, false)]
    [InlineData(2, 2, Operator.Smaller, false)]
    [InlineData(2, 3, Operator.Smaller, true)]
    [InlineData(null, 1, Operator.Smaller, false)]
    [InlineData(2, null, Operator.Smaller, false)]
    [InlineData(2, 1, Operator.SmallerOrEqual, false)]
    [InlineData(2, 2, Operator.SmallerOrEqual, true)]
    [InlineData(2, 3, Operator.SmallerOrEqual, true)]
    [InlineData(null, 1, Operator.SmallerOrEqual, false)]
    [InlineData(2, null, Operator.SmallerOrEqual, false)]
    [InlineData("A", null, Operator.IsNull, false)]
    [InlineData("", null, Operator.IsNull, false)]
    [InlineData(null, null, Operator.IsNull, true)]
    [InlineData("A", null, Operator.IsNotNull, true)]
    [InlineData("", null, Operator.IsNotNull, true)]
    [InlineData(null, null, Operator.IsNotNull, false)]
    [InlineData("A", null, Operator.IsNullOrEmpty, false)]
    [InlineData("", null, Operator.IsNullOrEmpty, true)]
    [InlineData(" ", null, Operator.IsNullOrEmpty, false)]
    [InlineData(null, null, Operator.IsNullOrEmpty, true)]
    [InlineData("A", null, Operator.IsNotNullOrEmpty, true)]
    [InlineData("", null, Operator.IsNotNullOrEmpty, false)]
    [InlineData(" ", null, Operator.IsNotNullOrEmpty, true)]
    [InlineData(null, null, Operator.IsNotNullOrEmpty, false)]
    [InlineData("A", null, Operator.IsNullOrWhiteSpace, false)]
    [InlineData("", null, Operator.IsNullOrWhiteSpace, true)]
    [InlineData(" ", null, Operator.IsNullOrWhiteSpace, true)]
    [InlineData(null, null, Operator.IsNullOrWhiteSpace, true)]
    [InlineData("A", null, Operator.IsNotNullOrWhiteSpace, true)]
    [InlineData("", null, Operator.IsNotNullOrWhiteSpace, false)]
    [InlineData(" ", null, Operator.IsNotNullOrWhiteSpace, false)]
    [InlineData(null, null, Operator.IsNotNullOrWhiteSpace, false)]
    [InlineData("Pizza", "x", Operator.Contains, false)]
    [InlineData("Pizza", "a", Operator.Contains, true)]
    [InlineData("Pizza", "A", Operator.Contains, true)]
    [InlineData(null, "x", Operator.Contains, false)]
    [InlineData("Pizza", "x", Operator.NotContains, true)]
    [InlineData("Pizza", "a", Operator.NotContains, false)]
    [InlineData("Pizza", "A", Operator.NotContains, false)]
    [InlineData(null, "A", Operator.NotContains, false)]
    [InlineData("Pizza", "x", Operator.StartsWith, false)]
    [InlineData("Pizza", "p", Operator.StartsWith, true)]
    [InlineData("Pizza", "P", Operator.StartsWith, true)]
    [InlineData(null, "x", Operator.StartsWith, false)]
    [InlineData("Pizza", "x", Operator.NotStartsWith, true)]
    [InlineData("Pizza", "p", Operator.NotStartsWith, false)]
    [InlineData("Pizza", "P", Operator.NotStartsWith, false)]
    [InlineData(null, "P", Operator.NotStartsWith, false)]
    [InlineData("Pizza", "x", Operator.EndsWith, false)]
    [InlineData("Pizza", "a", Operator.EndsWith, true)]
    [InlineData("Pizza", "A", Operator.EndsWith, true)]
    [InlineData(null, "x", Operator.EndsWith, false)]
    [InlineData("Pizza", "x", Operator.NotEndsWith, true)]
    [InlineData("Pizza", "a", Operator.NotEndsWith, false)]
    [InlineData("Pizza", "A", Operator.NotEndsWith, false)]
    [InlineData(null, "A", Operator.NotEndsWith, false)]
    public void IsItemValid_Returns_Correct_Result_On_Contains_Condition(object? leftValue,
                                                                         object? rightValue,
                                                                         Operator @operator,
                                                                         bool expectedResult)
    {
        // Arrange
        var leftExpressionMock = new Mock<IConstantExpression>();
        leftExpressionMock.SetupGet(x => x.Value).Returns(leftValue);
        var leftExpression = leftExpressionMock.Object;
        var rightExpressionMock = new Mock<IConstantExpression>();
        rightExpressionMock.SetupGet(x => x.Value).Returns(rightValue);
        var rightExpression = rightExpressionMock.Object;
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.Operator).Returns(@operator);
        conditionMock.SetupGet(x => x.LeftExpression).Returns(leftExpression);
        conditionMock.SetupGet(x => x.RightExpression).Returns(rightExpression);
        _expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                                .Returns<object?, IExpression>((_, expression) =>
                                {
                                    if (expression == leftExpression)
                                    {
                                        return leftExpression.Value;
                                    }
                                    if (expression == rightExpression)
                                    {
                                        return rightExpression.Value;
                                    }

                                    return null;
                                });

        // Act
        var actual = CreateSut().Evaluate(null, new[] { conditionMock.Object });

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public void IsItemValid_Works_Correctly_On_Equals_With_Sequences()
    {
        // Arrange
        var leftValue = new ValueCollection<string>(new [] { "1", "2", "3" });
        var rightValue = new ValueCollection<string>(new [] { "1", "2", "3" });
        var leftExpressionMock = new Mock<IConstantExpression>();
        leftExpressionMock.SetupGet(x => x.Value).Returns(leftValue);
        var leftExpression = leftExpressionMock.Object;
        var rightExpressionMock = new Mock<IConstantExpression>();
        rightExpressionMock.SetupGet(x => x.Value).Returns(rightValue);
        var rightExpression = rightExpressionMock.Object;
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.Operator).Returns(Operator.Equal);
        conditionMock.SetupGet(x => x.LeftExpression).Returns(leftExpression);
        conditionMock.SetupGet(x => x.RightExpression).Returns(rightExpression);
        _expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                                .Returns<object?, IExpression>((_, expression) =>
                                {
                                    if (expression == leftExpression)
                                    {
                                        return leftExpression.Value;
                                    }
                                    if (expression == rightExpression)
                                    {
                                        return rightExpression.Value;
                                    }

                                    return null;
                                });

        // Act
        var actual = CreateSut().Evaluate(null, new[] { conditionMock.Object });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsItemValid_Works_Correctly_On_Contains_With_Sequence_Of_Strings()
    {
        // Arrange
        var leftValue = new ValueCollection<string>(new[] { "1", "2", "3" });
        var rightValue = "2";
        var leftExpressionMock = new Mock<IConstantExpression>();
        leftExpressionMock.SetupGet(x => x.Value).Returns(leftValue);
        var leftExpression = leftExpressionMock.Object;
        var rightExpressionMock = new Mock<IConstantExpression>();
        rightExpressionMock.SetupGet(x => x.Value).Returns(rightValue);
        var rightExpression = rightExpressionMock.Object;
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.Operator).Returns(Operator.Contains);
        conditionMock.SetupGet(x => x.LeftExpression).Returns(leftExpression);
        conditionMock.SetupGet(x => x.RightExpression).Returns(rightExpression);
        _expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                                .Returns<object?, IExpression>((_, expression) =>
                                {
                                    if (expression == leftExpression)
                                    {
                                        return leftExpression.Value;
                                    }
                                    if (expression == rightExpression)
                                    {
                                        return rightExpression.Value;
                                    }

                                    return null;
                                });

        // Act
        var actual = CreateSut().Evaluate(null, new[] { conditionMock.Object });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsItemValid_Works_Correctly_On_Contains_With_Sequence_Of_Ints()
    {
        // Arrange
        var leftValue = new ValueCollection<int>(new[] { 1, 2, 3 });
        var rightValue = 2;
        var leftExpressionMock = new Mock<IConstantExpression>();
        leftExpressionMock.SetupGet(x => x.Value).Returns(leftValue);
        var leftExpression = leftExpressionMock.Object;
        var rightExpressionMock = new Mock<IConstantExpression>();
        rightExpressionMock.SetupGet(x => x.Value).Returns(rightValue);
        var rightExpression = rightExpressionMock.Object;
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.Operator).Returns(Operator.Contains);
        conditionMock.SetupGet(x => x.LeftExpression).Returns(leftExpression);
        conditionMock.SetupGet(x => x.RightExpression).Returns(rightExpression);
        _expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IExpression>()))
                                .Returns<object?, IExpression>((_, expression) =>
                                {
                                    if (expression == leftExpression)
                                    {
                                        return leftExpression.Value;
                                    }
                                    if (expression == rightExpression)
                                    {
                                        return rightExpression.Value;
                                    }

                                    return null;
                                });

        // Act
        var actual = CreateSut().Evaluate(null, new[] { conditionMock.Object });

        // Assert
        actual.Should().BeTrue();
    }
}
