namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringConcatenateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpressionBuilder().Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("At least one expression is required");
    }

    [Fact]
    public void Evaluate_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpressionBuilder()
            .AddExpressions("a", "b", "c")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("abc");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_One_Of_The_Expressions_Return_Error()
    {
        // Arrange
        var sut = new StringConcatenateExpression([new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom"))]);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expressions_Is_Empty()
    {
        // Arrange
        var sut = new StringConcatenateExpressionBuilder().BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("At least one expression is required");
    }

    [Fact]
    public void EvaluateTyped_Returns_Concatated_String_When_All_Expressions_Contain_String_Value()
    {
        // Arrange
        var sut = new StringConcatenateExpressionBuilder()
            .AddExpressions("a", "b", "c")
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("abc");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new StringConcatenateExpressionBuilder()
            .AddExpressions("A", "B")
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<StringConcatenateExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(StringConcatenateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(StringConcatenateExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.UsesContext.ShouldBeTrue();
        result.ContextIsRequired.ShouldBe(false);
    }
}
