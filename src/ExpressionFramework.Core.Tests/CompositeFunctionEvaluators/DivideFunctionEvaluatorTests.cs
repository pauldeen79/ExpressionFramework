﻿namespace ExpressionFramework.Core.Tests.CompositeFunctionEvaluators;

public class DivideCompositeFunctionTests
{
    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Byte()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result );

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int16()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const short value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int32()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Int64()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const long value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Single()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const float value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Double()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const double value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Value_Is_Decimal()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const decimal value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(10 / value);
    }

    [Fact]
    public void TryEvaluate_Returns_Null_When_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_Null_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new DivideCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new EmptyCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }
}