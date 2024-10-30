namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToCamelCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_CamelCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression("Camel").Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("camel");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(string.Empty).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(default(string)!).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_CamelCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression("Camel").BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("camel");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(string.Empty).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(default(string)!).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression("A").BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<ToCamelCaseExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ToCamelCaseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ToCamelCaseExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
