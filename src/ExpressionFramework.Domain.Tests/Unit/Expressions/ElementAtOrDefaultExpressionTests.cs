namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ElementAtOrDefaultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Expression_Is_Empty_Enumerable_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .WithIndexExpression(1)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeNull();
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Expression_Is_Empty_Enumerable_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .WithIndexExpression(1)
            .WithDefaultExpression("default value")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("default value");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(new TypedConstantResultExpressionBuilder<int>().WithValue(Result.Invalid<int>("Something bad happened")))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_IndexExpression_Returns_Error()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(new TypedConstantResultExpressionBuilder<int>().WithValue(Result.Error<int>("Something bad happened")))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeNull();
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(10)
            .WithDefaultExpression("default")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("default");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithIndexExpression(1)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(2);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ElementAtOrDefaultExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ElementAtOrDefaultExpression));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.Count.ShouldBe(3);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
