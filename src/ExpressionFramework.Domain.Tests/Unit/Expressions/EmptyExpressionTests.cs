namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EmptyExpressionTests
{
    [Fact]
    public void ValidateWithContext_Return_Empty_Sequence()
    {
        // Arrange
        var sut = new EmptyExpression();

        // Act
        var actual = sut.ValidateContext(null, new ValidationContext(sut));

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(EmptyExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EmptyExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
