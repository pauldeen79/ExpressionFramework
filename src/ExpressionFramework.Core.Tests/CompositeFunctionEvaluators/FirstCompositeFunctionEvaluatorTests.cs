namespace ExpressionFramework.Core.Tests.CompositeFunctionEvaluators;

public class FirstCompositeFunctionTests
{
    [Fact]
    public void TryEvaluate_Returns_Correct_Result_When_Item_Is_First()
    {
        // Arrange
        var sut = new FirstCompositeFunctionEvaluator();
        const byte value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new FirstCompositeFunction(), true, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeFalse();
        actual.Result.Should().Be(10);
    }

    [Fact]
    public void TryEvaluate_Returns_Null_Result_When_Item_Is_Not_First()
    {
        // Arrange
        var sut = new FirstCompositeFunctionEvaluator();
        const string value = "2";
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new FirstCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeTrue();
        actual.ShouldContinue.Should().BeFalse();
        actual.Result.Should().BeNull();
        actual.ErrorMessage.Should().Be("Not supposed to come here, as we said to stop!");
    }

    [Fact]
    public void TryEvaluate_Returns_NotSupported_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new FirstCompositeFunctionEvaluator();
        const int value = 2;
        var expression = new ConstantExpressionBuilder(value).Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(null, null, expression)).Returns(Result<object?>.Success(value));

        // Act
        var actual = sut.TryEvaluate(new MultiplyCompositeFunction(), false, 10, null, expressionEvaluatorMock.Object, expression);

        // Assert
        actual.IsSupported.Should().BeFalse();
        actual.ShouldContinue.Should().BeTrue();
        actual.Result.Should().BeNull();
    }
}
