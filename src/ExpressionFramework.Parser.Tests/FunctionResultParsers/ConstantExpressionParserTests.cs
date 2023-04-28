namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public class ConstantExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public ConstantExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Error("Kaboom"));
    }
    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new ConstantExpressionParser(_parserMock.Object);
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new ConstantExpressionParser(_parserMock.Object);
        var functionParseResult = new FunctionParseResult("Constant", new[] { new LiteralArgument("some value") }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("some value");
    }

    [Fact]
    public void Parse_Returns_Error_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new ConstantExpressionParser(_parserMock.Object);
        var functionParseResult = new FunctionParseResult("Constant", new[] { new FunctionArgument(new FunctionParseResult("UNKNOWN_FUNCTION", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
