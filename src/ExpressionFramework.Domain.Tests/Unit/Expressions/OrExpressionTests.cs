﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OrExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_FirstExpression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression("not a boolean"), new TrueExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("FirstExpression must be of type boolean");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_SecondExpression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression(true), new EmptyExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SecondExpression must be of type boolean");
    }

    [Fact]
    public void Evaluate_Returns_Success_When_FirstExpression_And_SecondExpression_Are_Both_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression(false), new TrueExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_FirstExpression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression("not a boolean"), new TrueExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("FirstExpression must be of type boolean");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_SecondExpression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression(true), new EmptyExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SecondExpression must be of type boolean");
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_When_FirstExpression_And_SecondExpression_Are_Both_Boolean()
    {
        // Arrange
        var sut = new OrExpression(new ConstantExpression(false), new TrueExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new OrExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OrExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OrExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
