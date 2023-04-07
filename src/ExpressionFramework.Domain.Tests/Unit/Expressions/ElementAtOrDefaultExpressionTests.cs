namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ElementAtOrDefaultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new EmptyExpression(), new TypedConstantExpression<int>(1), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(12345), new TypedConstantExpression<int>(1), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Expression_Is_Empty_Enumerable_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(Enumerable.Empty<object>()), new TypedConstantExpression<int>(1), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Expression_Is_Empty_Enumerable_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(Enumerable.Empty<object>()), new TypedConstantExpression<int>(1), new ConstantExpression("default value"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default value");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new InvalidExpression(new ConstantExpression("Something bad happened")), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_IndexExpression_Returns_Error()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ErrorExpression(new ConstantExpression("Something bad happened")), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression("No integer value"), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression(10), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_IndexExpression_DefaultValue()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression(10), new ConstantExpression("default"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default");
    }
    
    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new ElementAtOrDefaultExpression(new ConstantExpression(new[] { 1, 2, 3 }), new TypedConstantExpression<int>(1), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(2);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new ElementAtOrDefaultExpressionBase(new EmptyExpression(), new EmptyExpression(), null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ElementAtOrDefaultExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ElementAtOrDefaultExpression));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
