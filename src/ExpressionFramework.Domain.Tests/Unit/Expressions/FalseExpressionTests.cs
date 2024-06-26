﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FalseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Value_True()
    {
        // Arrange
        var sut = new FalseExpression();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_With_Value_True()
    {
        // Arrange
        var sut = new FalseExpression();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new FalseExpression();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<FalseExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(FalseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(FalseExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
