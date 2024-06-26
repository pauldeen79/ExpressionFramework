﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NotExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpressionBuilder()
            .WithExpression(false)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var expression = new NotExpression(new TypedConstantResultExpression<bool>(Result.Error<bool>("Kaboom")));

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression(new TypedConstantExpression<bool>(true));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new NotExpression(new TrueExpression());

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<NotExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(NotExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(NotExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
