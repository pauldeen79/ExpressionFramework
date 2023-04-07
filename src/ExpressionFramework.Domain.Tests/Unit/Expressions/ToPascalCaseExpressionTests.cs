namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToPascalCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_PascalCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression("Pascal"));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("pascal");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression(default(object?)));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_PascalCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression("Pascal"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("pascal");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression(string.Empty));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().Should().BeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToPascalCaseExpression(new ConstantExpression(default(object?)));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new ToPascalCaseExpressionBase(new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
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
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
