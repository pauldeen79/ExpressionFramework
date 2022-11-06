namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TodayExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(dateTime);
        var sut = new TodayExpression(dateTimeProvider.Object);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(dateTime.Date);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(dateTime);
        var sut = new TodayExpression(dateTimeProvider.Object);

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(dateTime.Date);
    }
}
