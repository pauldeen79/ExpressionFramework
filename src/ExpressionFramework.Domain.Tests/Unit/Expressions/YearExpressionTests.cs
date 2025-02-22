namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class YearExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpressionBuilder()
            .WithExpression(new DateTime(2010, 1, 2, 0, 0, 0, DateTimeKind.Local))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(2010);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new YearExpressionBuilder()
            .WithExpression(new DateTime(2010, 1, 2, 0, 0, 0, DateTimeKind.Local))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(2010);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new YearExpressionBuilder()
            .WithExpression(DateTime.Today)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<YearExpression>();
    }
}
