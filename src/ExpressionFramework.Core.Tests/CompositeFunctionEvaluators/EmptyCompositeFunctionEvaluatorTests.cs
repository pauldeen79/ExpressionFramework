namespace ExpressionFramework.Core.Tests.CompositeFunctionEvaluators;

public class EmptyCompositeFunctionEvaluatorTests
{
    [Fact]
    public void TryEvaluate_Returns_Null_When_Function_Is_Of_Correct_Type()
    {
        // Arrange
        var sut = new EmptyCompositeFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new EmptyCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_Null_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new EmptyCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(value);

        // Act
        var actual = sut.TryEvaluate(new DivideCompositeFunction(), 10, null, expressionEvaluatorMock.Object, expression, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }
}
