namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AnyExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context cannot be empty");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.Evaluate(123);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Context_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.Evaluate(Enumerable.Empty<object>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AnyExpression(new InvalidExpression("Something bad happened"));

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
        var sut = new AnyExpression(new ErrorExpression("Something bad happened"));

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
        var sut = new AnyExpression(new ConstantExpression("None boolean value"));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpression(new DelegateExpression(x => x is int i && i > 10));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new DelegateExpression(x => x is int i && i > 1));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context cannot be empty");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.EvaluateTyped(123);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Context_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.EvaluateTyped(Enumerable.Empty<object>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AnyExpression(new InvalidExpression("Something bad happened"));

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_PredicateExpression_Returns_Error()
    {
        // Arrange
        var sut = new AnyExpression(new ErrorExpression("Something bad happened"));

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Something bad happened");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_PredicateExpression_Returns_Non_Boolean_Value()
    {
        // Arrange
        var sut = new AnyExpression(new ConstantExpression("None boolean value"));

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpression(new DelegateExpression(x => x is int i && i > 10));

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpression(new DelegateExpression(x => x is int i && i > 1));

        // Act
        var result = sut.EvaluateTyped(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.ValidateContext(new[] { 1, 2, 3 });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new AnyExpression(null);

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context is not of type enumerable" });
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
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
