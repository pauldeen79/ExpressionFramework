﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class MonthExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new MonthExpression(new EmptyExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpression(new ConstantExpression(123));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpression(new ConstantExpression(new DateTime(2010, 1, 2)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new MonthExpression(new EmptyExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Not_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpression(new ConstantExpression(123));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpression(new ConstantExpression(new DateTime(2010, 1, 2)));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new MonthExpressionBase(new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new MonthExpression(new ConstantExpression(DateTime.Today));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
    }
}