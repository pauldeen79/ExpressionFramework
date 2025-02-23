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
        actual.GetValueOrThrow().ShouldBeEquivalentTo("camel");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(string.Empty).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(default(string)!).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_CamelCase_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression("Camel").BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("camel");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(string.Empty).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression(default(string)!).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new ToCamelCaseExpressionBuilder().WithExpression("A").BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<ToCamelCaseExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ToCamelCaseExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ToCamelCaseExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
