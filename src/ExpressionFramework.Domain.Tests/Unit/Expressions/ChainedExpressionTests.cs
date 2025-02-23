namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ChainedExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_First_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom")),
                new EmptyExpressionBuilder()
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Second_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new EmptyExpressionBuilder(),
                new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom"))
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Context_As_Value_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder().BuildTyped();

        // Act
        var actual = expression.Evaluate("test");

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("test");
    }

    [Fact]
    public void Evaluate_Returns_Last_Result_When_Multiple_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions(new ContextExpressionBuilder(), new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .BuildTyped();

        // Act
        var result = expression.Evaluate("hello");

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("HELLO");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ChainedExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ChainedExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.UsesContext.ShouldBeTrue();
        result.ContextIsRequired.ShouldBe(false);
    }
}
