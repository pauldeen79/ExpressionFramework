namespace ExpressionFramework.Core.Tests.FunctionEvaluators;

public class DivideFunctionEvaluatorTests
{
    [Fact]
    public void TryParse_Return_False_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        var functionMock = new Mock<IExpressionFunction>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(functionMock.Object, null, null, expressionEvaluatorMock.Object, out var _);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Byte()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Int16()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const short value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Int32()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Int64()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const long value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Single()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const float value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Double()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const double value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Decimal()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const decimal value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, 10, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(10 / value);
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new DivideFunctionEvaluator();
        const string value = "hello world"; //string, cannot be divided!
        var expression = new ConstantExpressionBuilder(value).Build();
        var function = new DivideFunction(expression, null);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(function, value, null, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().BeNull();
    }
}
