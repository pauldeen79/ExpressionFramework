﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class DayExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new DayExpression(new DateTime(2010, 1, 2));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(2);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new DayExpression(new DateTime(2010, 1, 2));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(2);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new DayExpression(DateTime.Today);

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<DayExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new DayExpressionBase(new TypedConstantExpression<DateTime>(DateTime.Today));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success()
    {
        // Arrange
        var expression = new DayExpression(DateTime.Today);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
    }
}
