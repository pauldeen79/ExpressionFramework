namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FirstOrDefaultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

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
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .Build();

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
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .WithDefaultExpression("default value")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default value");
    }
    
    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression_No_DefaultValue()
    {
        // Arrange
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Default_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression_DefaultValue()
    {
        // Arrange
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .WithDefaultExpression("default")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("default");
    }
    
    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new FirstOrDefaultExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 1)
            .Build();

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
        var expression = new FirstOrDefaultExpressionBase(new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object>()), null, null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(FirstOrDefaultExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(FirstOrDefaultExpression));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
