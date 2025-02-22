namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TakeExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var sut = new TakeExpression(new TypedDelegateResultExpression<IEnumerable>(_ => Result.Error<IEnumerable>("Kaboom")), new TypedConstantExpression<int>(1));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_CountExpression_Returns_Error()
    {
        // Arrange
        var sut = new TakeExpression(new TypedConstantExpression<IEnumerable>(new object[] { "A", "B", 1, "C" }), new TypedDelegateResultExpression<int>(_ => Result.Error<int>("Kaboom")));

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
        var sut = new TakeExpressionBuilder()
            .WithExpression(new object[] { "A", "B", 1, "C" })
            .WithCountExpression(2)
            .Build();

        // Act
        var result = sut.Evaluate().TryCast<IEnumerable<object>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.GetValueOrThrow().ToArray().ShouldBeEquivalentTo(new object[] { "A", "B" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new TakeExpressionBuilder()
            .WithExpression(new object[] { "A", "B", 1, "C" })
            .WithCountExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<TakeExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TakeExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TakeExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
