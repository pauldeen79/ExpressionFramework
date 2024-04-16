namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedChainedExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_First_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new TypedChainedExpressionBuilder<string>()
            .AddExpressions
            (
                new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom")),
                new EmptyExpressionBuilder()
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Second_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new TypedChainedExpressionBuilder<string>()
            .AddExpressions
            (
                new EmptyExpressionBuilder(),
                new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom"))
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Context_As_Value_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new TypedChainedExpressionBuilder<string>().BuildTyped();

        // Act
        var actual = expression.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeEquivalentTo("test");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Default_Value_When_No_Expressions_Are_Provided_And_Context_Is_Of_Wrong_Type()
    {
        // Arrange
        var expression = new TypedChainedExpressionBuilder<string>().BuildTyped();

        // Act
        var actual = expression.Evaluate(1);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Last_Result_When_Multiple_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new TypedChainedExpressionBuilder<string>().AddExpressions(new TypedContextExpressionBuilder<string>(), new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>())).BuildTyped();

        // Act
        var result = expression.EvaluateTyped("hello");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("HELLO");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedChainedExpression<string>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TypedChainedExpression<string>));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
