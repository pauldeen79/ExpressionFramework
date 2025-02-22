namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class WhereExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new WhereExpression(new DefaultExpression<IEnumerable>(), new TypedDelegateExpression<bool>(x => x is string));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var sut = new WhereExpression(new TypedConstantResultExpression<IEnumerable>(Result.Error<IEnumerable>("Kaboom")), new TypedDelegateExpression<bool>(x => x is string));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Predicate_Returns_Error()
    {
        // Arrange
        var sut = new WhereExpressionBuilder()
            .WithExpression(new object[] { "A", "B", 1, "C" })
            .WithPredicateExpression(new TypedConstantResultExpressionBuilder<bool>().WithValue(Result.Error<bool>("Kaboom")))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new WhereExpressionBuilder()
            .WithExpression(new object[] { "A", "B", 1, "C" })
            .WithPredicateExpression(x => x is string)
            .Build();

        // Act
        var result = sut.Evaluate().TryCast<IEnumerable<object>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.GetValueOrThrow().ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new WhereExpression(new DefaultExpression<IEnumerable>(), new TypedDelegateExpression<bool>(x => x is string));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void EvaluateTyped_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new WhereExpressionBuilder()
            .WithExpression(new object[] { "A", "B", 1, "C" })
            .WithPredicateExpression(x => x is string)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.GetValueOrThrow().ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new WhereExpressionBuilder()
            .WithExpression(new[] { 1, 2, 3 })
            .WithPredicateExpression(true)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<WhereExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(WhereExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(WhereExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(3);
        result.ContextIsRequired.ShouldBeNull();
    }
}
