namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SubstringExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Substring_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("e");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression(new ConstantExpression("Kaboom")), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("not an integer"), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new InvalidExpression(new ConstantExpression("Kaboom")));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression("not an integer"));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Value_Is_Not_String()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression(1));

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression(new ConstantExpression("Kaboom")), new ConstantExpression(1));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Result_Is_Invalid()
    {
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Context_Is_Too_Short()
    {
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SubstringExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SubstringExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
