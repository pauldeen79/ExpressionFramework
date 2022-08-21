namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProviderTests
{
    [Fact]
    public void TryEvaluate_Returns_False_When_Expression_Is_Not_A_CompositeExpression()
    {
        // Arrange
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        var sut = new CompositeExpressionEvaluatorProvider(new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known()
    {
        // Arrange
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1 + 2;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>(), out tempResult)).Returns(true);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Unknown()
    {
        // Arrange
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object? tempResult = null;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>(), out tempResult)).Returns(false);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known_And_No_Expressions_Are_Provided()
    {
        // Arrange
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object? tempResult = null;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>(), out tempResult)).Returns(true);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }
}
