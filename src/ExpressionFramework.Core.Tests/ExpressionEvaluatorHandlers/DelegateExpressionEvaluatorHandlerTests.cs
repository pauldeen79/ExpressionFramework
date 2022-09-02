namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class DelegateExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_False_When_Expression_Is_Not_A_DelegateExpression()
    {
        // Arrange
        var sut = new DelegateExpressionEvaluatorHandler();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_True_When_Expression_Is_A_DelegateExpression()
    {
        // Arrange
        var sut = new DelegateExpressionEvaluatorHandler();
        var expression = new DelegateExpressionBuilder()
            .WithValueDelegate(new Func<IDelegateExpressionRequest, IDelegateExpressionResponse>(_ => new DelegateExpressionResponseBuilder().WithResult(12345).Build()))
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
