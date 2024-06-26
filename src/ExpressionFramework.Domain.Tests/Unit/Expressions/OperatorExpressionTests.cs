﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OperatorExpressionTests
{
    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression()
    {
        // Arrange
        var @operator = new EqualsOperatorBuilder();
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression("A")
            .WithRightExpression("B")
            .WithOperator(@operator)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression_Typed()
    {
        // Arrange
        var @operator = new EqualsOperatorBuilder();
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression("A")
            .WithRightExpression("B")
            .WithOperator(@operator)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression(false)
            .WithRightExpression(true)
            .WithOperator(new EqualsOperatorBuilder())
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<OperatorExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OperatorExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OperatorExpression));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }
}
