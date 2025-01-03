﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SelectExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new TypedContextExpression<string>(), default));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void EvaluateTyped_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new TypedContextExpression<string>(), default));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(["A", "B", "C"]);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(new[] { "a", "b", "c" })
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<SelectExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SelectExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SelectExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
