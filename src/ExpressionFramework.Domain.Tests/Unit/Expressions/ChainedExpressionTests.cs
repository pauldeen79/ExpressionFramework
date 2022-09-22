namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ChainedExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new ErrorExpressionBuilder().WithErrorMessage("Kaboom"),
                new EmptyExpressionBuilder()
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Sucess_With_Context_As_Value_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder().Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }
}
