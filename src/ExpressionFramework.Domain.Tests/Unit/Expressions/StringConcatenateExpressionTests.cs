namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringConcatenateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<string>());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("At least one expression is required");
    }

    [Fact]
    public void Evaluate_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[]
        {
            "a",
            "b",
            "c"
        });

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("abc");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<string>());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("At least one expression is required");
    }

    [Fact]
    public void EvaluateTyped_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[]
        {
            "a",
            "b",
            "c"
        });

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("abc");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { "A", "B" });

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<StringConcatenateExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new StringConcatenateExpressionBase(Enumerable.Empty<ITypedExpression<string>>());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new StringConcatenateExpression(Enumerable.Empty<ITypedExpression<string>>());

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(StringConcatenateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(StringConcatenateExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
