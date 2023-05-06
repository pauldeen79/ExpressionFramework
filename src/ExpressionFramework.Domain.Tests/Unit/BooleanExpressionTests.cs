namespace ExpressionFramework.Domain.Tests.Unit;

public class BooleanExpressionTests
{
    [Fact]
    public void EvaluateBooleanCombinations_Returns_Error_When_First_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantResultExpression<bool>(Result<bool>.Error("Kaboom")), new TypedConstantExpression<bool>(true), (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateBooleanCombinations_Returns_Error_When_Second_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), new TypedConstantResultExpression<bool>(Result<bool>.Error("Kaboom")), (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
