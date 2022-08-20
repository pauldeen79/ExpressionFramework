using CrossCutting.Common.Results;

namespace ExpressionFramework.Core.Tests.CompositeFunctions;

public class DivideCompositeFunctionTests
{
    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Byte()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Int16()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const short value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Int32()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Int64()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const long value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Single()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const float value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Double()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const double value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Value_Is_Decimal()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const decimal value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().Be(10 / value);
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new DivideCompositeFunction();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.Combine(10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.Should().BeOfType<Result>();
        var result = actual as Result;
        result!.ErrorMessage.Should().Be("Items are of wrong type. Only byte, short, int, long, float, double and decimal are supported. Found types: [System.Int32] and [System.String]");
    }
}
