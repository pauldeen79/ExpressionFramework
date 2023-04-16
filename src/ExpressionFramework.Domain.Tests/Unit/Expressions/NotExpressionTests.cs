namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NotExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression(new ConstantExpression(false));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Of_Wrong_Type()
    {
        // Arrange
        var sut = new NotExpression(new EmptyExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type boolean");
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression(new ConstantExpression(true));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Of_Wrong_Type()
    {
        // Arrange
        var sut = new NotExpression(new EmptyExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type boolean");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new NotExpressionBase(new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new NotExpression(new ConstantExpression(true));

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
        var sut = new ReflectionExpressionDescriptorProvider(typeof(NotExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(NotExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
