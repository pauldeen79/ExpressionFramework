namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class LeftExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression("test", 2);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("te");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpression(_ => string.Empty, _ => 2);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new LeftExpression(new EmptyExpression(), new ConstantExpression(2));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("test"), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Evaluation_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("test"), new ConstantExpression("no integer in here"));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("test"), new ConstantExpression(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("te");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression(string.Empty), new ConstantExpression(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new LeftExpression(new EmptyExpression(), new ConstantExpression(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("test"), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Evaluation_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new LeftExpression(new ConstantExpression("test"), new ConstantExpression("no integer in here"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new LeftExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new LeftExpression(new ConstantExpression("test"), new ConstantExpression(2));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
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
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
