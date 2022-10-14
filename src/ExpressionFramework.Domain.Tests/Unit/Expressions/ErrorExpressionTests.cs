namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ErrorExpressionTests
{
    [Fact]
    public void Evaluate_Returns_ErrorResult()
    {
        // Assert
        var sut = new ErrorExpression("Error message");

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Error message");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new ErrorExpression(new ErrorExpression("Kaboom"));

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
        var sut = new ErrorExpression(new ConstantExpression(1));

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
        var sut = new ErrorExpression(new InvalidExpression("error message"));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "ErrorMessageExpression returned an invalid result. Error message: error message" });
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_ErrorMessageExpression_Returns_Non_String_Value()
    {
        // Assert
        var sut = new ErrorExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "ErrorMessageExpression did not return a string" });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ErrorExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ErrorExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
