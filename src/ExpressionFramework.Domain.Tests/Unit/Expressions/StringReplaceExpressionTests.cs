namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringReplaceExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_FindExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression("Hello world"), new ErrorExpression(new ConstantExpression("Kaboom")), new ConstantExpression("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_FindExpression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression("Hello world"), new ConstantExpression(default(object?)), new ConstantExpression("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("FindExpression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_ReplaceExpression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression("Hello world"), new ConstantExpression("e"), new ConstantExpression(default(object?)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("ReplaceExpression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression(default(object?)), new ConstantExpression("e"), new ConstantExpression("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Replaced_Value_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression("Hello world"), new ConstantExpression("e"), new ConstantExpression("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world".Replace("e", "f"));
    }

    [Fact]
    public void EvaluateTyped_Returns_Replaced_Value_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringReplaceExpression(new ConstantExpression("Hello world"), new ConstantExpression("e"), new ConstantExpression("f"));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world".Replace("e", "f"));
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new StringReplaceExpressionBase(new EmptyExpression(), new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }
}
