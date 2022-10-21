namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class BooleanConstantExpressionTests
{
    [Fact]
    public void Can_Evaluate_Boolean()
    {
        // Arrange
        var sut = new BooleanConstantExpression(true);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Can_Evaluate_Boolean_Typed()
    {
        // Arrange
        var sut = new BooleanConstantExpression(true);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeTrue();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(BooleanConstantExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(BooleanConstantExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
