namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class LeftExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(2)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("te");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression(string.Empty)
            .WithLengthExpression(2)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedDelegateResultExpression<string>(_ => Result<string>.Error("Kaboom")), new TypedConstantExpression<int>(1));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedDelegateResultExpression<int>(_ => Result<int>.Error("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(2));

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
        var sut = new LeftExpression(new TypedConstantExpression<string>(string.Empty), new TypedConstantExpression<int>(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedDelegateResultExpression<int>(_ => Result<int>.Error("Kaboom")));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<LeftExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new LeftExpressionBase(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(1));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(2));

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
