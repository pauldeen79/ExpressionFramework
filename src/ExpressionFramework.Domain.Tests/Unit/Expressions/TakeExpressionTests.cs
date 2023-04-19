namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TakeExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var sut = new TakeExpression(new TypedDelegateResultExpression<IEnumerable>(_ => Result<IEnumerable>.Error("Kaboom")), new TypedConstantExpression<int>(1));

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
        var sut = new TakeExpression(new TypedConstantExpression<IEnumerable>(new object[] { "A", "B", 1, "C" }), new TypedDelegateResultExpression<int>(_ => Result<int>.Error("Kaboom")));

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
        var sut = new TakeExpression(new object[] { "A", "B", 1, "C" }, 2);

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
        var sut = new TakeExpression(new object[] { "A", "B", 1, "C" }, 1);

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<TakeExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new TakeExpressionBase(new TypedConstantExpression<IEnumerable>(new object[] { "A", "B", 1, "C" }), new TypedDelegateResultExpression<int>(_ => Result<int>.Error("Kaboom")));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new TakeExpression(new object[] { "A", "B", 1, "C" }, 2);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
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
