namespace ExpressionFramework.Core.Tests.AggregateFunctionEvaluators;

public class PlusAggregateFunctionTests
{
    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Byte()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int16()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const short value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int32()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int64()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const long value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Single()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const float value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Double()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const double value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Decimal()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const decimal value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(10 + value);
    }

    [Fact]
    public void TryEvaluate_Returns_Null_Result_When_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeFalse();
        actual.GetResultValue().Should().BeNull();
        actual.ErrorMessage.Should().Be("Type of current value is not supported");
    }

    [Fact]
    public void TryEvaluate_Returns_Null_Result_When_Value_Is_First()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        var expression = new EmptyExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(new PlusAggregateFunction(), true, 2, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeTrue();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().Be(2);
        actual.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_NotSupported_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new PlusAggregateFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new MultiplyAggregateFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported().Should().BeFalse();
        actual.ShouldContinue().Should().BeTrue();
        actual.GetResultValue().Should().BeNull();
    }
}

