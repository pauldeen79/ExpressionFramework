﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class DelegateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new DelegateExpression(_ => "ok");

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("ok");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DelegateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(DelegateExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
