namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringLengthExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Length_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression("some"));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(4);
    }

    [Fact]
    public void Evaluate_Returns_0_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(0);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression(null));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Length_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression("some"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().Be(4);
    }

    [Fact]
    public void EvaluateTyped_Returns_0_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().Be(0);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new StringLengthExpression(new ConstantExpression(null));

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(StringLengthExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(StringLengthExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
