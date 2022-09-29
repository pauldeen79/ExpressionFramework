namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class EnumerableNotContainsOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(EnumerableNotContainsOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EnumerableNotContainsOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeTrue();
        result.RightValueTypeName.Should().NotBeEmpty();
        result.ReturnValues.Should().HaveCount(2);
    }
}
