namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ConstantExpressionTests
{
    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new ConstantExpressionBase(null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ConstantExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ConstantExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
