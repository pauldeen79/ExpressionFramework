namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TrimStartExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(" trim ").Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("trim ");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder()
            .WithExpression("0trim0")
            .WithTrimCharsExpression(['0'])
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("trim0");
    }

    [Fact]
    public void Evaluate_Returns_Trimmed_Expression_With_TrimChars_When_TrimChars_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantExpression<string>(" trim "), new TypedConstantExpression<char[]>(default(char[])!));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("trim ");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(string.Empty).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(default(string)!).Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")), null);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }
    [Fact]
    public void Evaluate_Returns_Error_When_TrimCharsExpression_Returns_Error()
    {
        // Arrange
        var sut = new TrimStartExpression(new TypedConstantExpression<string>("0trim0"), new TypedConstantResultExpression<char[]>(Result.Error<char[]>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(" trim ").BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBe("trim ");
    }

    [Fact]
    public void EvaluateTyped_Returns_Trimmed_Expression_With_TrimChars_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder()
            .WithExpression("0trim0")
            .WithTrimCharsExpression(['0'])
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBe("trim0");
    }

    [Fact]
    public void EvaluateTyped_Returns_EmptyString_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(string.Empty).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TrimStartExpressionBuilder().WithExpression(default(string)!).BuildTyped();

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
        var sut = new TrimStartExpressionBuilder().WithExpression("A").BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<TrimStartExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrimStartExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TrimStartExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
