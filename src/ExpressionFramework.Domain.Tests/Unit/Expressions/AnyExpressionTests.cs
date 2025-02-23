namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AnyExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithPredicateExpression(false)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_False_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_False_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(false);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 1)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new AnyExpressionBuilder().WithExpression(default(IEnumerable)!).BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_False_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new AnyExpressionBuilder().WithExpression(new[] { 1, 2, 3 }).BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 1)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(true);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new AnyExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<AnyExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AnyExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(AnyExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(3);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
