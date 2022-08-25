namespace ExpressionFramework.Core.Tests.Default;

public class ExpressionEvaluatorTests
{
    private readonly ExpressionEvaluatorProviderMock _expressionEvaluatorProviderMock = new();
    private readonly FunctionEvaluatorMock _functionEvaluatorMock = new();

    [Fact]
    public void Evaluate_Throws_When_Expression_Is_Not_Supported()
    {
        // Arrange
        var sut = CreateSut();
        var expression = new Mock<IExpression>().Object;

        // Act
        var result = sut.Evaluate(null, null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unsupported expression: [IExpressionProxy]");
    }

    [Fact]
    public void Evaluate_Throws_When_Function_Is_Not_Supported()
    {
        // Arrange
        var sut = CreateSut();
        var functionMock = new Mock<IExpressionFunction>();
        var expressionMock = new Mock<IExpression>();
        expressionMock.SetupGet(x => x.Function).Returns(functionMock.Object);
        var expression = expressionMock.Object;
        _expressionEvaluatorProviderMock.Delegate = new ((_, _, _, _) => new Tuple<bool, object?>(true, null));

        // Act
        var result = sut.Evaluate(null, null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unsupported function: [IExpressionFunctionProxy]");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Value_Without_Function()
    {
        // Arrange
        var sut = CreateSut();
        var expression = new Mock<IExpression>().Object;
        _expressionEvaluatorProviderMock.Delegate = new((_, _, _, _) => new Tuple<bool, object?>(true, 12345));

        // Act
        var result = sut.Evaluate(null, null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(12345);
    }

    [Fact]
    public void Evaluate_Returns_Correct_Value_With_Function()
    {
        // Arrange
        var sut = CreateSut();
        var functionMock = new Mock<IExpressionFunction>();
        var expressionMock = new Mock<IExpression>();
        expressionMock.SetupGet(x => x.Function).Returns(functionMock.Object);
        var expression = expressionMock.Object;
        _expressionEvaluatorProviderMock.Delegate = new((_, _, _, _) => new Tuple<bool, object?>(true, 12345));
        _functionEvaluatorMock.Delegate = new((_, value, _, _, _) => new Tuple<bool, object?>(true, Convert.ToInt32(value) * 2));

        // Act
        var result = sut.Evaluate(null, null, expression);

        // Assert

        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(12345 * 2);
    }

    private ExpressionEvaluator CreateSut()
        => new(new[] { _expressionEvaluatorProviderMock },
               new[] { _functionEvaluatorMock });
}
