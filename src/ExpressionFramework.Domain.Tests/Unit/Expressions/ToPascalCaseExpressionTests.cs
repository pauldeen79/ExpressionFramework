namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToPascalCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_PascalCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression("pascal").Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("Pascal");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression(string.Empty).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression(default(string)!).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_PascalCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression("pascal").BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("Pascal");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression(string.Empty).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToPascalCaseExpressionBuilder().WithExpression(default(string)!).BuildTyped();

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
        var sut = new ToPascalCaseExpressionBuilder().WithExpression("A").BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<ToPascalCaseExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ToPascalCaseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ToPascalCaseExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
