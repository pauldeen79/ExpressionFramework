namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class InvalidExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_Result_With_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression("Error message", new[] { new ValidationError("Validation error message", new[] { "Member" }) });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEquivalentTo(sut.ValidationErrors);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression("Error message"));

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Error message");
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new ErrorExpression("Kaboom"));

        // Act
        var result = sut.Evaluate(null);

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
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("ErrorMessageExpression did not return a string");
        result.ValidationErrors.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_ErrorMessageExpression_Returns_Invalid_Result()
    {
        // Assert
        var sut = new InvalidExpression(new InvalidExpression("error message"));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "ErrorMessageExpression returned an invalid result. Error message: error message" });
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_ErrorMessageExpression_Returns_Non_String_Value()
    {
        // Assert
        var sut = new InvalidExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "ErrorMessageExpression did not return a string" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Assert
        var sut = new InvalidExpression("Some error message");

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Should().BeEmpty();
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
