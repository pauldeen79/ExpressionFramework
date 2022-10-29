namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class LeftExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LeftValue_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("te");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new ErrorExpression("Kaboom"));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Evaluation_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("no integer in here"));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_LeftValue_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("te");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.EvaluateTyped(string.Empty);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new LeftExpression(2);

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new ErrorExpression("Kaboom"));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Evaluation_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("no integer in here"));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(LeftExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(LeftExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
