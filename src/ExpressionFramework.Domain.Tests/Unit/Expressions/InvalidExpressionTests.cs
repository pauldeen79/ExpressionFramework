namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class InvalidExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_Result_With_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression("Error message", new[] { new ValidationError("Validation error message", new[] { "Member" }) });

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEquivalentTo(sut.ValidationErrorExpressions!.Select(x => x.EvaluateTyped().Value));
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression("Error message");

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors_And_ErrorMessage()
    {
        // Assert
        var sut = new InvalidExpression(default(string)!);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().BeNull();
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new TypedConstantExpression<string>("Ignored"), new[] { new TypedConstantResultExpression<ValidationError>(Result<ValidationError>.Error("Kaboom")) });

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
        var sut = new InvalidExpression(new TypedConstantResultExpression<string>(Result<string>.Error("Kaboom")), Enumerable.Empty<ITypedExpression<ValidationError>>());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new InvalidExpressionBase(new DefaultExpression<string>(), Enumerable.Empty<ITypedExpression<ValidationError>>());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new InvalidExpression("Something went wrong", Enumerable.Empty<ValidationError>());

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
