﻿namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class IsNotNullOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(IsNotNullOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(IsNotNullOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeFalse();
        result.RightValueTypeName.Should().BeNull();
        result.ReturnValues.Should().ContainSingle();
    }
}
