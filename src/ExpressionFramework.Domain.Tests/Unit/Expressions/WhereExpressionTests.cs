namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class WhereExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new WhereExpression(new EmptyExpression(), new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new WhereExpression(new ConstantExpression(1), new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Predicate_Does_Not_Return_A_Boolean_Value()
    {
        // Arrange
        var sut = new WhereExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new DelegateExpression(_ => "not a boolean value"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Predicate()
    {
        // Arrange
        var sut = new WhereExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ErrorExpression(new ConstantExpression("Kaboom")));

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
        var sut = new WhereExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new WhereExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new WhereExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new DelegateExpression(x => x is string));

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(WhereExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(WhereExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextIsRequired.Should().BeNull();
    }
}
