namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToUpperCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_UpperCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate("Upper");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("UPPER");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_UpperCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.EvaluateTyped("Upper");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("UPPER");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.EvaluateTyped(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ToUpperCaseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ToUpperCaseExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
