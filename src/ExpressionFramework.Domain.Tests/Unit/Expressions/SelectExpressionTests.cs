namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SelectExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpression(default(IEnumerable)!, new ToUpperCaseExpression(new ContextExpression()));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new ContextExpression()));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpression(default(IEnumerable)!, new ToUpperCaseExpression(new ContextExpression()));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void EvaluateTyped_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new ContextExpression()));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SelectExpression(new[] { "a", "b", "c" }, new ToUpperCaseExpression(new ContextExpression()));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<SelectExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new SelectExpressionBase(new TypedConstantExpression<IEnumerable>(default(IEnumerable)!), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new ContextExpression()));

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SelectExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SelectExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
