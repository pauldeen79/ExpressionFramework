namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class Int32ConstantExpressionTests
{
    [Fact]
    public void Can_Evaluate_Int32()
    {
        // Arrange
        var sut = new Int32ConstantExpression(1);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Can_Evaluate_Int32_Typed()
    {
        // Arrange
        var sut = new Int32ConstantExpression(1);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(Int32ConstantExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(Int32ConstantExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
