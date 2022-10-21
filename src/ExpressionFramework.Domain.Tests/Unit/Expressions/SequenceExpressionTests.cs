namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SequenceExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_One_Expression_Returns_Invalid()
    {
        // Arrange
        var sut = new SequenceExpression(new Expression[] { new ConstantExpression(1), new ConstantExpression(2), new InvalidExpression("Message") });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Message");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_One_Expression_Returns_Error()
    {
        // Arrange
        var sut = new SequenceExpression(new Expression[] { new ConstantExpression(1), new ConstantExpression(2), new ErrorExpression("Kaboom") });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Empty_Sequence_When_Expressions_Are_Empty()
    {
        // Arrange
        var sut = new SequenceExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.Evaluate(null).TryCast<IEnumerable<object?>>();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Filled_Sequence_When_Expressions_Are_Not_Empty()
    {
        // Arrange
        var sut = new SequenceExpression(new Expression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) });

        // Act
        var result = sut.Evaluate(null).TryCast<IEnumerable<object?>>();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { 1, 2, 3 });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SequenceExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SequenceExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
