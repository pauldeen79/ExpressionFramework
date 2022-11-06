namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NowExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(dateTime);
        var sut = new NowExpression(dateTimeProvider.Object);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(dateTime);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(dateTime);
        var sut = new NowExpression(dateTimeProvider.Object);

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(dateTime);
    }
}
