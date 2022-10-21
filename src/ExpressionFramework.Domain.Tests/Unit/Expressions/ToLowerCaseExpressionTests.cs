namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToLowerCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LowerCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate("LOWER");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("lower");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_LowerCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.EvaluateTyped("LOWER");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("lower");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.EvaluateTyped(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Value_Is_Not_String()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ToLowerCaseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ToLowerCaseExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
