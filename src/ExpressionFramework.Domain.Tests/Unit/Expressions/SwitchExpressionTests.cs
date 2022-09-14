namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SwitchExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder().WithExpression(new ErrorExpressionBuilder().WithErrorMessage("Kaboom")))
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Default_When_No_Cases_Are_Present()
    {
        // Arrange
        var expression = new SwitchExpressionBuilder().Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }
}
