﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedDelegateResultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new TypedDelegateResultExpression<int>(_ => Result.Success(34));

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(34);
    }

    [Fact]
    public void EvaluateTyped_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new TypedDelegateResultExpression<int>(_ => Result.Success(34));

        // Act
        var result = sut.EvaluateTyped("not used");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(34);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedDelegateResultExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TypedDelegateResultExpression<int>));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
