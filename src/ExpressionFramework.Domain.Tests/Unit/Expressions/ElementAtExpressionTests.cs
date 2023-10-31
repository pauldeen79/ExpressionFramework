﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ElementAtExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ElementAtExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .WithIndexExpression(1)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Index is outside the bounds of the array");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new ElementAtExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), new TypedConstantResultExpression<int>(Result.Invalid<int>("Something bad happened")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Index is outside the bounds of the array");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(1)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(2);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new ElementAtExpressionBase(new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object>()), new TypedConstantExpression<int>(1));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ElementAtExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ElementAtExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
