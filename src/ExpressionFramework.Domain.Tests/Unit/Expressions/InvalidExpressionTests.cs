namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class InvalidExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_Result_With_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression("Error message"), new[] { new ValidationError("Validation error message", new[] { "Member" }) }.Select(x => new ConstantExpression(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEquivalentTo(sut.ValidationErrorExpressions.Select(x => x.Evaluate().Value));
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression("Error message"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new ErrorExpression(new ConstantExpression("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ValidationErrors_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression("Error message"), new[] { new ErrorExpression(new ConstantExpression("Kaboom")) });

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_ErrorMessageExpression_Returns_Non_String_Value()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().BeNull();
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new InvalidExpressionBase(new EmptyExpression(), Enumerable.Empty<Expression>());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new InvalidExpression(new ConstantExpression("Something went wrong"), Enumerable.Empty<Expression>());

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(InvalidExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(InvalidExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
