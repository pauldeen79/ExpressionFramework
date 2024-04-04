﻿namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class NotEndsWithOperatorTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_LeftValue_Is_Null()
    {
        // Act
        var result = new NotEndsWithOperator().Evaluate(null, new EmptyExpression(), new ConstantExpression("B"));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_RightValue_Is_Null()
    {
        // Act
        var result = new NotEndsWithOperator().Evaluate(null, new ConstantExpression("A"), new EmptyExpression());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_False_When_RightValue_Is_StringEmpty()
    {
        // Act
        var result = new NotEndsWithOperator().Evaluate(null, new ConstantExpression("A"), new ConstantExpression(string.Empty));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(NotEndsWithOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(NotEndsWithOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeTrue();
        result.RightValueTypeName.Should().NotBeEmpty();
        result.ReturnValues.Should().HaveCount(2);
    }
}
