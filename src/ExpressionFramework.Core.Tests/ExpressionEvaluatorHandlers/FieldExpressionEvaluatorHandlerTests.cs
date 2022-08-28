namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class FieldExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_False_When_Expression_Is_Not_A_FieldExpression()
    {
        // Arrange
        var valueProviderMock = new Mock<IValueProvider>();
        var sut = new FieldExpressionEvaluatorHandler(valueProviderMock.Object);
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_True_When_Expression_Is_A_FieldExpression()
    {
        // Arrange
        var valueProviderMock = new Mock<IValueProvider>();
        var sut = new FieldExpressionEvaluatorHandler(valueProviderMock.Object);
        var expressionMock = new Mock<IFieldExpression>();
        expressionMock.SetupGet(x => x.Function).Returns(default(IExpressionFunction));
        expressionMock.SetupGet(x => x.FieldName).Returns("Test");
        valueProviderMock.Setup(x => x.GetValue(It.IsAny<object?>(), "Test")).Returns(Result<object?>.Success(12345));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().Be(12345);
    }
}
