namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EmptyExpressionTests
{
    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new EmptyExpressionBase();

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new EmptyExpression();

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
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
