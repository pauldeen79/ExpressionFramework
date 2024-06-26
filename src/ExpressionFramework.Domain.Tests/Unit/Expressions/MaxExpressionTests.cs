﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class MaxExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new MaxExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SelectorExpression_Returns_Error()
    {
        // Arrange
        var sut = new MaxExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new MaxExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_SelectorExpression_Result_When_SelectorExpression_Is_Not_Null()
    {
        // Arrange
        var sut = new MaxExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2 }), new DelegateExpression(x => Convert.ToInt32(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(2);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(MaxExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(MaxExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextIsRequired.Should().BeNull();
    }
}
