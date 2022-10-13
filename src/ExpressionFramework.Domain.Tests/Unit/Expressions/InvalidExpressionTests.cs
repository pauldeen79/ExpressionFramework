namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class InvalidExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_Result()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression("Error message"), new[] { new ValidationError("Validation error message", new[] { "Member" }) });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEquivalentTo(sut.ValidationErrors);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(InvalidExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(InvalidExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
