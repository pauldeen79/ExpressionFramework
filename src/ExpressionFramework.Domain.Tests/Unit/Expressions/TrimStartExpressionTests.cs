﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TrimStartExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(" trim ").Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("trim ");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder()
            .WithExpression("0trim0")
            .WithTrimCharsExpression(['0'])
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("trim0");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_TrimChars_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantExpression<string>(" trim "), new TypedConstantExpression<char[]>(default(char[])!));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("trim ");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(string.Empty).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(default(string)!).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")), null);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }
    [Fact]
    public void Evaluate_Returns_Error_When_TrimCharsExpression_Returns_Error()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantExpression<string>("0trim0"), new TypedConstantResultExpression<char[]>(Result.Error<char[]>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(" trim ").BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().Be("trim ");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder()
            .WithExpression("0trim0")
            .WithTrimCharsExpression(['0'])
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().Be("trim0");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(string.Empty).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(default(string)!).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression("A").BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<TrimStartExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrimStartExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TrimStartExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
