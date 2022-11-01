namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TakeExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new TakeExpression(new EmptyExpression(), new ConstantExpression(2));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new TakeExpression(new ConstantExpression(1), new ConstantExpression(2));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_CountExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new TakeExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ConstantExpression("non integer value"));

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
        var sut = new TakeExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ErrorExpression(new ConstantExpression("Kaboom")));

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
        var sut = new TakeExpression(new ConstantExpression(new object[] { "A", "B", 1, "C" }), new ConstantExpression(2));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B" });
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
