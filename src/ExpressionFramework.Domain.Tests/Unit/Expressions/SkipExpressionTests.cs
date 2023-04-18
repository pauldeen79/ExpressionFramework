namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SkipExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SkipExpression(default(object?), 1);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SkipExpression(_ => 1, _ => 1);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_CountExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ConstantExpression("non integer value"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("CountExpression is not of type integer");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_CountExpression_Returns_Error()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

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
        var sut = new SkipExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new TypedConstantExpression<int>(1));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new object[] { "B", 1, "C" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new TypedConstantExpression<int>(1));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<SkipExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new SkipExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new SkipExpression(new ConstantExpression(new[] { "a", "b", "c" }), new ConstantExpression(1));

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SkipExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SkipExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
