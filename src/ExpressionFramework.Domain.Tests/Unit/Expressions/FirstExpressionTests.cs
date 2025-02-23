namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FirstExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new FirstExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new FirstExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Enumerable is empty");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new FirstExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("None of the items conform to the supplied predicate");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new FirstExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new FirstExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 1)
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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(FirstExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(FirstExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(3);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
