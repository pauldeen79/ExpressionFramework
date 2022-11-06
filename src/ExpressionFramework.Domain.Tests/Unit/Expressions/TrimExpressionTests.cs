namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TrimExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(" trim "));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("trim");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression("0trim0"), new ConstantExpression(new[] { '0' }));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("trim");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(null));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_TrimCharsExpression_Returns_Error()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression("0trim0"), new ErrorExpression(new ConstantExpression("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(" trim "));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("trim");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression("0trim0"), new ConstantExpression(new[] { '0' }));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().Be("trim");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimExpression(new ConstantExpression(null));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrimExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TrimExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
