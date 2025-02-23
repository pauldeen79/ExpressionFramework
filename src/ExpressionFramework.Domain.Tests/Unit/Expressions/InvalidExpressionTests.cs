namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class InvalidExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_Result_With_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpressionBuilder()
            .WithErrorMessageExpression("Error message")
            .AddValidationErrorExpressions(new ValidationError("Validation error message", ["Member"]))
            .BuildTyped();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Error message");
        result.ValidationErrors.ToArray().ShouldBeEquivalentTo(sut.ValidationErrorExpressions!.Select(x => x.EvaluateTyped().Value).ToArray());
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors()
    {
        // Assert
        var sut = new InvalidExpressionBuilder()
            .WithErrorMessageExpression("Error message")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Error message");
        result.ValidationErrors.ShouldBeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_Without_ValidationErrors_And_ErrorMessage()
    {
        // Assert
        var sut = new InvalidExpressionBuilder().WithErrorMessageExpression(default(string)!).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBeNull();
        result.ValidationErrors.ShouldBeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new TypedConstantExpression<string>("Ignored"), [new TypedConstantResultExpression<ValidationError>(Result.Error<ValidationError>("Kaboom"))]);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ValidationErrors_Returns_Error()
    {
        // Assert
        var sut = new InvalidExpression(new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")), Enumerable.Empty<ITypedExpression<ValidationError>>());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(InvalidExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(InvalidExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
