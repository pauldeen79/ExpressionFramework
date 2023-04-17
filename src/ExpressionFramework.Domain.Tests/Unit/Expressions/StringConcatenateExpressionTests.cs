namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringConcatenateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpression(Enumerable.Empty<object?>());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("At least one expression is required");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Contains_Non_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new Func<object?, object?>[] { _ => false });

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expressions must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expressions_Returns_One_Item_With_Error_Result()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ErrorExpression(new ConstantExpression("Kaboom")) });

        // Act
        var result = sut.Evaluate();

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
        var result = sut.Evaluate();

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
        result.ErrorMessage.Should().Be("Expressions must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Expressions_Returns_One_Item_With_Error_Result()
    {
        // Arrange
        var sut = new StringConcatenateExpression(new[] { new ErrorExpression(new ConstantExpression("Kaboom")) });

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
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new StringConcatenateExpressionBase(Enumerable.Empty<Expression>());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new StringConcatenateExpression(Enumerable.Empty<Expression>());

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
