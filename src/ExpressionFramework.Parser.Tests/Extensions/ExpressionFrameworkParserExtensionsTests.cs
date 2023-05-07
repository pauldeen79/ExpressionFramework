namespace ExpressionFramework.Parser.Tests.Extensions;

public class ExpressionFrameworkParserExtensionsTests
{
    private readonly Mock<IExpressionFrameworkParser> _sut = new();
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();

    [Fact]
    public void Parse_Returns_Failure_When_Source_Parsing_Fails()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        _sut.Setup(x => x.Parse(It.IsAny<FunctionParseResult>(), It.IsAny<IFunctionParseResultEvaluator>(), It.IsAny<IExpressionParser>()))
            .Returns(Result<Expression>.Error("Kaboom"));

        // Act
        var result = _sut.Object.Parse<string>(functionParseResult, _evaluatorMock.Object, _expressionParserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Parse_Returns_Success_When_Source_Parsing_Succeeds_And_Result_Value_Is_Of_Correct_Type()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        _sut.Setup(x => x.Parse(It.IsAny<FunctionParseResult>(), It.IsAny<IFunctionParseResultEvaluator>(), It.IsAny<IExpressionParser>()))
            .Returns(Result<Expression>.Success(new TypedConstantExpression<string>("test")));

        // Act
        var result = _sut.Object.Parse<string>(functionParseResult, _evaluatorMock.Object, _expressionParserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [Fact]
    public void Parse_Returns_Invalid_When_Source_Parsing_Succeeds_And_Result_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        _sut.Setup(x => x.Parse(It.IsAny<FunctionParseResult>(), It.IsAny<IFunctionParseResultEvaluator>(), It.IsAny<IExpressionParser>()))
            .Returns(Result<Expression>.Success(new TypedConstantExpression<int>(1)));

        // Act
        var result = _sut.Object.Parse<string>(functionParseResult, _evaluatorMock.Object, _expressionParserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not a typed expression of type System.String");
    }
}
