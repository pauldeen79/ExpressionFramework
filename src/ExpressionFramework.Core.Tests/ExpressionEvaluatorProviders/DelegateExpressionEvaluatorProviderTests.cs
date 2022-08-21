namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class DelegateExpressionEvaluatorProviderTests
{
    [Fact]
    public void TryEvaluate_Returns_False_When_Expression_Is_Not_A_DelegateExpression()
    {
        // Arrange
        var sut = new DelegateExpressionEvaluatorProvider();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_DelegateExpression()
    {
        // Arrange
        var sut = new DelegateExpressionEvaluatorProvider();
        var expressionMock = new Mock<IDelegateExpression>();
        expressionMock.SetupGet(x => x.ValueDelegate).Returns(new Func<object?, IExpression, IExpressionEvaluator, object?>((_, _, _) => 12345));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(12345);
    }
}
