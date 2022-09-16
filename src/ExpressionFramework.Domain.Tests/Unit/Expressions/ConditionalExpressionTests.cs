namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ConditionalExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_From_Expression_Evaluation()
    {
        // Arrange
        var sut = new ConditionalExpression(new SingleEvaluatable(new ErrorExpression("Kaboom"), new EqualsOperator(), new EmptyExpression()), new EmptyExpression(), null);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
