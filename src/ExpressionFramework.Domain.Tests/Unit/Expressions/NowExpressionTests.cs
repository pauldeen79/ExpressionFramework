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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(dateTime);
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(dateTime);
    }

    [Fact]
    public void EvaluateTyped_Returns_Current_DateTime_Without_DateTimeProvider()
    {
        // Arrange
        var sut = new NowExpression(default(IDateTimeProvider?));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new NowExpression(default(IDateTimeProvider?));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<NowExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new NowExpressionBase(null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }
}
