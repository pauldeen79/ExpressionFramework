﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TrimEndExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimEndExpression();

        // Act
        var actual = sut.Evaluate(" trim ");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(" trim");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimEndExpression(new[] { '0' });

        // Act
        var actual = sut.Evaluate("0trim0");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("0trim");
    }
    [Fact]
    public void Evaluate_Returns_EmptyString_Wnen_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimEndExpression();

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Wnen_Context_Is_Null()
    {
        // Arrange
        var sut = new TrimEndExpression();

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Value_Is_Not_String()
    {
        // Arrange
        var sut = new TrimEndExpression();

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrimEndExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TrimEndExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
