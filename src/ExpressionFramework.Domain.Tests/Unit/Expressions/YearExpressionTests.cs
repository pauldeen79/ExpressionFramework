namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class YearExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new YearExpression(new EmptyExpression());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpression(new ConstantExpression(123));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpression(new ConstantExpression(new DateTime(2010, 1, 2)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(2010);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new YearExpression(new EmptyExpression());

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Not_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpression(new ConstantExpression(123));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type DateTime");
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpression(new ConstantExpression(new DateTime(2010, 1, 2)));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(2010);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new YearExpressionBase(new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }
}
