namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AnyExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AnyExpression(new EmptyExpression(), null);

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
        var sut = new AnyExpression(new ConstantExpression(123), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(Enumerable.Empty<object>()), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new InvalidExpression(new ConstantExpression("Something bad happened")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_PredicateExpression_Returns_Error()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ErrorExpression(new ConstantExpression("Something bad happened")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Non_Boolean_Value()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression("None boolean value"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new DelegateExpression(x => x is int i && i > 10));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new DelegateExpression(x => x is int i && i > 1));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AnyExpression(new EmptyExpression(), null);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(123), null);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(Enumerable.Empty<object>()), null);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new InvalidExpression(new ConstantExpression("Something bad happened")));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_PredicateExpression_Returns_Error()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ErrorExpression(new ConstantExpression("Something bad happened")));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_PredicateExpression_Returns_Non_Boolean_Value()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression("None boolean value"));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new DelegateExpression(x => x is int i && i > 10));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), null);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression(new[] { 1, 2, 3 }), new DelegateExpression(x => x is int i && i > 1));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AnyExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(AnyExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
