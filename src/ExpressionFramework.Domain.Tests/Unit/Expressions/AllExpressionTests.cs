namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AllExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AllExpression(new EmptyExpression(), new DelegateExpression(_ => false));

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
        var sut = new AllExpression(new ConstantExpression(1), new DelegateExpression(_ => false));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_True_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AllExpression(_ => Enumerable.Empty<object?>(), _ => false);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new InvalidExpression("Something bad happened"));

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
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ErrorExpression("Something bad happened"));

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
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression("None boolean value"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AllExpression(new[] { 1, 2, 3 }, x => x is int i && i > 10);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new AllExpression(new[] { 1, 2, 3 }, x => x is int i && i > 1);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AllExpression(new EmptyExpression(), new DelegateExpression(_ => false));

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
        var sut = new AllExpression(new ConstantExpression(123), new TypedConstantExpression<bool>(false));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_True_When_Context_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AllExpression(Enumerable.Empty<object>(), _ => false);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_PredicateExpression_Returns_Invalid()
    {
        // Arrange
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new InvalidExpression("Something bad happened"));

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
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ErrorExpression("Something bad happened"));

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
        var sut = new AllExpression(new ConstantExpression(new[] { 1, 2, 3 }), new ConstantExpression("None boolean value"));

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Predicate did not return a boolean value");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Enumerable_Context_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AllExpression(new[] { 1, 2, 3 }, x => x is int i && i > 10);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable()
    {
        // Arrange
        var sut = new AllExpression(new[] { 1, 2, 3 }, x => x is int i && i > 1);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new AllExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_ConstantExpression()
    {
        // Arrange
        var expression = new AllExpression(Enumerable.Empty<object>(), _ => false);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<IEnumerable>>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_DelegateExpression()
    {
        // Arrange
        var expression = new AllExpression(_ => Enumerable.Empty<object>(), _ => false);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedDelegateExpression<IEnumerable>>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AllExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(AllExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
