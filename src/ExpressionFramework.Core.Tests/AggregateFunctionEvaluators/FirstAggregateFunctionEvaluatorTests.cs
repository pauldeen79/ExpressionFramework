namespace ExpressionFramework.Core.Tests.AggregateFunctionEvaluators;

public class FirstAggregateFunctionTests
{
    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Item_Is_First()
    {
        // Arrange
        var sut = new FirstAggregateFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.Evaluate(new FirstAggregateFunction(), true, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeFalse();
        actual.GetResultValue().Should().Be(10);
    }

    [Fact]
    public void Evaluate_Returns_Null_Result_When_Item_Is_Not_First()
    {
        // Arrange
        var sut = new FirstAggregateFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.Evaluate(new FirstAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeFalse();
        actual.GetResultValue().Should().BeNull();
        actual.ErrorMessage.Should().Be("Not supposed to come here, as we said to stop!");
    }

    [Fact]
    public void Evaluate_Returns_NotSupported_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new FirstAggregateFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.Evaluate(new MultiplyAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeFalse();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().BeNull();
    }
}
