namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CountExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Zero_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new CountExpressionBuilder().WithExpression(Enumerable.Empty<object>()).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(0);
    }

    [Fact]
    public void Evaluate_Returns_Zero_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(0);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
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
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new CountExpressionBuilder().WithExpression(new DefaultExpressionBuilder<IEnumerable>()).BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void EvaluateTyped_Returns_Zero_When_Expression_Is_Empty_Enumerable()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(Enumerable.Empty<object>())
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(0);
    }

    [Fact]
    public void EvaluateTyped_Returns_Zero_When_Enumerable_Expression_Does_Not_Contain_Any_Item_That_Conforms_To_PredicateExpression()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 10)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(0);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_Without_Predicate()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(3);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_On_Filled_Enumerable_With_Predicate()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(x => x is int i && i > 1)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(2);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new CountExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<CountExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(CountExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(CountExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
