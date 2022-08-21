namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class ItemExpressionEvaluatorProviderTests
{
    [Fact]
    public void TryEvaluate_Returns_False_When_Expression_Is_Not_A_ItemExpression()
    {
        // Arrange
        var sut = new ItemExpressionEvaluatorProvider();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_ItemExpression()
    {
        // Arrange
        var sut = new ItemExpressionEvaluatorProvider();
        var expressionMock = new Mock<IItemExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(12345, null, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().Be(12345);
    }
}
