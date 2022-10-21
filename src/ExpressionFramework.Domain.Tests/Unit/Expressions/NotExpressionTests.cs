namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NotExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression();

        // Act
        var result = sut.Evaluate(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression();

        // Act
        var result = sut.EvaluateTyped(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Of_Wrong_Type()
    {
        // Arrange
        var sut = new NotExpression();

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context must be of type boolean");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(NotExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(NotExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
