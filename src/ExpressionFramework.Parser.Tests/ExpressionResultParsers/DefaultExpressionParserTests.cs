namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class DefaultExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    [Theory,
        InlineData("Wrong"),
        InlineData("Wrong<"),
        InlineData("Wrong>")]
    public void Parse_Returns_Continue_For_Wrong_FunctionName(string functionName)
    {
        // Arrange
        var parser = new DefaultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult(functionName, Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new DefaultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Default<System.Int32>", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, contextValue, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(default(int));
    }

    [Fact]
    public void Parse_Returns_Invalid_When_No_Type_Is_Specified()
    {
        // Arrange
        var parser = new DefaultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Default", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, contextValue, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("No type defined");
    }

    [Fact]
    public void Parse_Returns_Invalid_When_No_Type_Is_Specified_But_Brackets_Are_There()
    {
        // Arrange
        var parser = new DefaultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Default<>", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, contextValue, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("No type defined");
    }

    [Fact]
    public void Parse_Returns_Invalid_When_Unknown_Type_Is_Specified()
    {
        // Arrange
        var parser = new DefaultExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Default<UnknownType>", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, contextValue, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Unknown type: UnknownType");
    }
}
