namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class MonthExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpressionBuilder()
            .WithExpression(new DateTime(2010, 1, 2, 0, 0, 0, DateTimeKind.Local))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Result_When_Expression_Is_Of_Type_DateTime()
    {
        // Arrange
        var sut = new MonthExpressionBuilder()
            .WithExpression(new DateTime(2010, 1, 2, 0, 0, 0, DateTimeKind.Local))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new MonthExpressionBuilder()
            .WithExpression(DateTime.Today)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<MonthExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new MonthExpressionBase(new TypedConstantExpression<DateTime>(DateTime.Today));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }
}
