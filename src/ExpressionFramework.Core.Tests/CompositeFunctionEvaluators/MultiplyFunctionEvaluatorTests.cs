namespace ExpressionFramework.Core.Tests.CompositeFunctionEvaluators;

public class MultiplyCompositeFunctionTests
{
    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Byte()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int16()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const short value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int32()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int64()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const long value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Single()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const float value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Double()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const double value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Decimal()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const decimal value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().Be(10 * value);
    }

    [Fact]
    public void TryEvaluate_Returns_Null_Result_When_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeFalse();
        actual.Result.Should().BeNull();
        actual.ErrorMessage.Should().Be("Type of current value is not supported");
    }

    [Fact]
    public void TryEvaluate_Returns_NotSupported_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new MultiplyCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeFalse();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().BeNull();
    }
}

