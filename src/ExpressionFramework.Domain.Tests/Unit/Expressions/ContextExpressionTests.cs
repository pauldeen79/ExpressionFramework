namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ContextExpressionTests
{
    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new ContextExpressionBase();

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ContextExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ContextExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
