﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OperatorExpressionTests
{
    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression()
    {
        // Arrange
        var @operator = new EqualsOperator();
        var leftExpression = new ConstantExpression("A");
        var rightExpression = new ConstantExpression("B");
        var sut = new OperatorExpression(leftExpression, rightExpression, @operator);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression_Typed()
    {
        // Arrange
        var @operator = new EqualsOperator();
        var leftExpression = new ConstantExpression("A");
        var rightExpression = new ConstantExpression("B");
        var sut = new OperatorExpression(leftExpression, rightExpression, @operator);

        // Act
        var result = sut.EvaluateAsBoolean(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }
}
