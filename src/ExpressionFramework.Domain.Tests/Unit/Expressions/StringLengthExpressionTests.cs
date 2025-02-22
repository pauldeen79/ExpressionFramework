namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringLengthExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Length_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new StringLengthExpressionBuilder()
            .WithExpression("some")
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo(4);
    }

    [Fact]
    public void Evaluate_Returns_0_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new StringLengthExpressionBuilder()
            .WithExpression(string.Empty)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo(0);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new StringLengthExpression(new DefaultExpression<string>());

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Length_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new TypedConstantExpression<string>("some"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBe(4);
    }

    [Fact]
    public void EvaluateTyped_Returns_0_When_Expression_Is_EmptyString()
    {
        // Arrange
        var sut = new StringLengthExpression(new TypedConstantExpression<string>(string.Empty));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.GetValueOrThrow().ShouldBe(0);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new StringLengthExpression(new TypedConstantExpression<string>(default(string)!));

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
        var sut = new StringLengthExpressionBuilder()
            .WithExpression("A")
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<StringLengthExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(StringLengthExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(StringLengthExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
