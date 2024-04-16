namespace ExpressionFramework.Domain.Tests.Unit;

public class OperatorTests
{
    [Fact]
    public void Evaluate_Returns_Error_From_LeftExpression_When_Evaluation_Fails()
    {
        // Arrange
        var sut = new EqualsOperator();

        // Act
        var actual = sut.Evaluate(null, new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), new EmptyExpression());

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_From_RightExpression_When_Evaluation_Fails()
    {
        // Arrange
        var sut = new EqualsOperator();

        // Act
        var actual = sut.Evaluate(null, new EmptyExpression(), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }
}
