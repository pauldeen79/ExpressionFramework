namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class LastOrDefaultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Context_Is_Empty_Enumerable_No_DefaultValue()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.Evaluate(Enumerable.Empty<object>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Empty_Value_When_Context_Is_Empty_Enumerable_DefaultValue()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, new ConstantExpression("default value"));

        // Act
        var result = sut.Evaluate(Enumerable.Empty<object>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default value");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new InvalidExpression("Something bad happened"), null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_PredicateExpression_Returns_Error()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new ErrorExpression("Something bad happened"), null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Non_Boolean_Value()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new ConstantExpression("None boolean value"), null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new DelegateExpression(x => x is int i && i > 10), null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression_DefaultValue()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new DelegateExpression(x => x is int i && i > 10), new ConstantExpression("default"));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default");
    }
    
    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(new DelegateExpression(x => x is int i && i > 1), null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.ValidateContext(Enumerable.Empty<int>());

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new LastOrDefaultExpression(null, null);

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(LastOrDefaultExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(LastOrDefaultExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
