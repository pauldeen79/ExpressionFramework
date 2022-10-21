namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringConcatenateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("At least one expression is required");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Contains_Non_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ConstantExpression(false) });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expressions_Returns_One_Item_With_Error_Result()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ErrorExpression("Kaboom") });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[]
        {
            new ConstantExpression("a"),
            new ConstantExpression("b"),
            new ConstantExpression("c")
        });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("abc");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("At least one expression is required");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expressions_Contains_Non_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ConstantExpression(false) });

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Expressions_Returns_One_Item_With_Error_Result()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ErrorExpression("Kaboom") });

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[]
        {
            new ConstantExpression("a"),
            new ConstantExpression("b"),
            new ConstantExpression("c")
        });

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("abc");
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "At least one expression is required" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Expressions_Contains_Non_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ConstantExpression(false) });

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Expression must be of type string" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Expressions_Contains_Invalid_Result()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new InvalidExpression("error message") });

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "error message" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[]
        {
            new ConstantExpression("some string"),
            new ConstantExpression("some other string")
        });

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Should().BeEmpty();
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
        result.ContextDescription.Should().BeNull();
        result.ContextIsRequired.Should().BeNull();
        result.ContextTypeName.Should().BeNull();
    }
}
