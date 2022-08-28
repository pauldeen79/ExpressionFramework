namespace ExpressionFramework.Core.Tests.Default;

public class ExpressionEvaluatorTests
{
    private readonly Mock<IExpressionEvaluatorHandler> _expressionEvaluatorMock = new();
    private readonly FunctionEvaluatorMock _functionEvaluatorMock = new();

    [Fact]
    public void Evaluate_Returns_Unsupported_When_Expression_Is_Not_Supported()
    {
        // Arrange
        var sut = CreateSut();
        var expression = new Mock<IExpression>().Object;
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.NotSupported());
        // Act
        var result = sut.Evaluate(null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unsupported expression: [IExpressionProxy]");
    }

    [Fact]
    public void Evaluate_Returns_Unsupported_When_Function_Is_Not_Supported()
    {
        // Arrange
        var sut = CreateSut();
        var functionMock = new Mock<IExpressionFunction>();
        var expressionMock = new Mock<IExpression>();
        expressionMock.SetupGet(x => x.Function).Returns(functionMock.Object);
        var expression = expressionMock.Object;
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.Success(default));

        // Act
        var result = sut.Evaluate(null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unsupported function: [IExpressionFunctionProxy]");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_InnerExpression_Returns_Error()
    {
        // Arrange
        var sut = CreateSut();
        var expressionMock = new Mock<IExpression>();
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.Error("Kaboom"));
        // Act
        var result = sut.Evaluate(null, expressionMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionProvider_Returns_Error()
    {
        // Arrange
        var sut = CreateSut();
        var expression = new Mock<IExpression>().Object;
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.Error("Kaboom"));
        // Act
        var result = sut.Evaluate(null, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Value_Without_Function()
    {
        // Arrange
        var sut = CreateSut();
        var expression = new Mock<IExpression>().Object;
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.Success(12345));

        // Act
        var result = sut.Evaluate(null, expression);

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
        _expressionEvaluatorMock.Setup(x => x.Handle(It.IsAny<object?>(),
                                                     It.IsAny<IExpression>(),
                                                     It.IsAny<IExpressionEvaluator>()))
                                .Returns(Result<object?>.Success(12345));
        _functionEvaluatorMock.Delegate = new((_, value, _, _, _) => new Tuple<bool, object?>(true, Convert.ToInt32(value) * 2));

        // Act
        var result = sut.Evaluate(null, expression);

        // Assert

        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(12345 * 2);
    }

    private ExpressionEvaluator CreateSut()
        => new(new[] { _expressionEvaluatorMock.Object },
               new[] { _functionEvaluatorMock });
}
