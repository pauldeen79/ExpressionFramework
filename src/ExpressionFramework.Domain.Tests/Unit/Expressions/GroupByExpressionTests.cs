namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class GroupByExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new GroupByExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithKeySelectorExpression(new DelegateExpressionBuilder().WithValue(x => x?.ToString()?.Length ?? 0))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Expression()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantResultExpression<IEnumerable>(Result<IEnumerable>.Error("Kaboom")), new TypedConstantExpression<string>("Kaboom"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Grouped_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "cc" }), new DelegateExpression(x => x?.ToString()?.Length ?? 0));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "a", "b", "cc" }.GroupBy(x => x.Length));
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new GroupByExpressionBase(new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object>()), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new GroupByExpressionBuilder()
            .WithExpression(new[] { "a", "b", "cc" })
            .WithKeySelectorExpression(new DelegateExpressionBuilder().WithValue(x => x?.ToString()?.Length ?? 0))
            .Build();

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(GroupByExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(GroupByExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
