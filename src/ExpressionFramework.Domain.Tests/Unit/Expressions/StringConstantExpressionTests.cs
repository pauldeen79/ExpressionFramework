namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringConstantExpressionTests
{
    [Fact]
    public void Can_Evaluate_String()
    {
        // Arrange
        var sut = new StringConstantExpression("test");

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("test");
    }

    [Fact]
    public void Can_Evaluate_String_Typed()
    {
        // Arrange
        var sut = new StringConstantExpression("test");

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("test");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(StringConstantExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(StringConstantExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
