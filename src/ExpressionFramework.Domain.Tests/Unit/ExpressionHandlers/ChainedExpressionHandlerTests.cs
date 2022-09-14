namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class ChainedExpressionHandlerTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions(new ErrorExpressionBuilder().WithErrorMessage("Kaboom"))
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder().Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("No expressions found");
    }
}
