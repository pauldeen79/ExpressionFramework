namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SubstringExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Substring_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpression("test", 1, 1);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("e");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(_ => string.Empty, _ => 1, _ => 1);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(new EmptyExpression(), new DefaultExpression<int>(), new DefaultExpression<int>());

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new InvalidExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression("not an integer"), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression(1), new InvalidExpression("Kaboom"));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression(1), new ConstantExpression("not an integer"));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_Substring_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpression("test", 1, 1);

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("e");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(string.Empty, 1, 1);

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(new EmptyExpression(), new DefaultExpression<int>(), new DefaultExpression<int>());

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new InvalidExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression("not an integer"), new ConstantExpression(1));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression(1), new InvalidExpression("Something bad happened"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("test"), new ConstantExpression(1), new ConstantExpression("not an integer"));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SubstringExpression("AB", 1, 1);

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<SubstringExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new SubstringExpressionBase(new EmptyExpression(), new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_ConstantExpression()
    {
        // Arrange
        var expression = new SubstringExpression("Hello world", 1, 1);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_DelegateExpression()
    {
        // Arrange
        var expression = new SubstringExpression(_ => "Hello world", _ => 1, _ => 1);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedDelegateExpression<string>>();
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
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
