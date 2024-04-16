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
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_CountExpression_Returns_Error()
    {
        // Arrange
        var sut = new TakeExpression(new TypedConstantExpression<IEnumerable>(new object[] { "A", "B", 1, "C" }), new TypedDelegateResultExpression<int>(_ => Result.Error<int>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
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
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B" });
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
        actual.Should().BeOfType<TakeExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TakeExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TakeExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
