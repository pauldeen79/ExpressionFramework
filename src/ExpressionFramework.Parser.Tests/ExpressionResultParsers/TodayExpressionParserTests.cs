namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class TodayExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public TodayExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns<FunctionParseResult, IExpressionParser, object?>((result, parser, context)
                        => result.FunctionName == "Context"
                            ? Result<object?>.Success(context)
                            : Result<object?>.Error("Kaboom"));
        _parserMock.Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<IFormatProvider>(), It.IsAny<object?>()))
                   .Returns<string, IFormatProvider, object?>((value, formatProvider, context) => Result<object?>.Success(value));
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new TodayExpressionParser();
        var contextValue = new Mock<IDateTimeProvider>().Object;
        var functionParseResult = new FunctionParseResult("Today", new[] { new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)) }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TodayExpression>();
    }

    [Fact]
    public void Parse_Returns_Error_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new TodayExpressionParser();
        var functionParseResult = new FunctionParseResult("Today", new[] { new FunctionArgument(new FunctionParseResult("UNKNOWN_FUNCTION", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
