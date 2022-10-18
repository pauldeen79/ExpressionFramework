namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ElementAtExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate(Enumerable.Empty<object>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Index is outside the bounds of the array");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new ElementAtExpression(new InvalidExpression("Something bad happened"));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(10));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Index is outside the bounds of the array");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(2);
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(new[] { 1, 2, 3 });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new ElementAtExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ElementAtExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ElementAtExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
