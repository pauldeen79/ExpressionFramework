namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class ConstantResultExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public ConstantResultExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns<FunctionParseResult, IExpressionParser, object?>((result, parser, context) => result.FunctionName switch
                        {
                            "MyResult" => Result<object?>.Success(Result<string>.Success("My value")),
                            "UNKNOWN_FUNCTION" => Result<object?>.Error("Kaboom"),
                            _ => Result<object?>.Error($"Unknown function: {result.FunctionName}")
                        });
        _parserMock.Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<IFormatProvider>(), It.IsAny<object?>()))
                   .Returns<string, IFormatProvider, object?>((value, formatProvider, context) => Result<object?>.Success(value));
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new ConstantResultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new ConstantResultExpressionParser();
        var functionParseResult = new FunctionParseResult("ConstantResult", new[] { new FunctionArgument(new FunctionParseResult("MyResult", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("My value");
    }

    [Fact]
    public void Parse_Returns_Error_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new ConstantResultExpressionParser();
        var functionParseResult = new FunctionParseResult("ConstantResult", new[] { new FunctionArgument(new FunctionParseResult("UNKNOWN_FUNCTION", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
