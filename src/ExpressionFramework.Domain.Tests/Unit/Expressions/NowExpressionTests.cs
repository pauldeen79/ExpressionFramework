﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NowExpressionTests : TestBase
{
    [Fact]
    public void Evaluate_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = Fixture.Freeze<IDateTimeProvider>();
        dateTimeProvider.GetCurrentDateTime().Returns(dateTime);
        var sut = new NowExpression(dateTimeProvider);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(dateTime);
    }


    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var dateTimeProvider = Fixture.Freeze<IDateTimeProvider>();
        dateTimeProvider.GetCurrentDateTime().Returns(dateTime);
        var sut = new NowExpression(dateTimeProvider);

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(dateTime);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime_Without_DateTimeProvider()
    {
        // Arrange
        var sut = new NowExpression(default(IDateTimeProvider?));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeInRange(DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new NowExpression(default(IDateTimeProvider?));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<NowExpression>();
    }
}
