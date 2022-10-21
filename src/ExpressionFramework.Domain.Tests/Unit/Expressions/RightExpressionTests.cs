namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class RightExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LeftValue_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new RightExpression(2);

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("st");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new RightExpression(2);

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
        var sut = new RightExpression(2);

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
        var sut = new RightExpression(new ErrorExpression("Kaboom"));

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
        var sut = new RightExpression(new ConstantExpression("no integer in here"));

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
        var sut = new RightExpression(2);

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("st");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new RightExpression(2);

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
        var sut = new RightExpression(2);

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
        var sut = new RightExpression(new ErrorExpression("Kaboom"));

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
        var sut = new RightExpression(new ConstantExpression("no integer in here"));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Value_Is_Not_String()
    {
        // Arrange
        var sut = new RightExpression(2);

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Returns_Invalid_Result()
    {
        // Arrange
        var sut = new RightExpression(new InvalidExpression("kaboom"));

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "LengthExpression returned an invalid result. Error message: kaboom" });
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new RightExpression(new ConstantExpression("not an integer"));

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "LengthExpression did not return an integer" });
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new RightExpression(2);

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Length must refer to a location within the string" });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(RightExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(RightExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
