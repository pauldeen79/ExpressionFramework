namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TodayExpressionTests : TestBase
{
    [Fact]
    public void Evaluate_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = Fixture.Freeze<IDateTimeProvider>();
        dateTimeProvider.GetCurrentDateTime().Returns(dateTime);
        var sut = new TodayExpression(dateTimeProvider);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(dateTime.Date);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = Fixture.Freeze<IDateTimeProvider>();
        dateTimeProvider.GetCurrentDateTime().Returns(dateTime);
        var sut = new TodayExpression(dateTimeProvider);

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(dateTime.Date);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime_Without_DateTimeProvider()
    {
        // Arrange
        var sut = new TodayExpression(default(IDateTimeProvider));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeInRange(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(1));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new TodayExpression(default(IDateTimeProvider));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<TodayExpression>();
    }
}
