namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public class LeftExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();

    public LeftExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>()))
                      .Returns<FunctionParseResult>(result => result.FunctionName == "MyFunction"
                        ? Result<object?>.Success("4")
                        : Result<object?>.NotSupported("Only Parsed result function is supported"));
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new LeftExpressionParser();
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, default);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new LeftExpressionParser();
        var functionParseResult = new FunctionParseResult("Left", new FunctionParseResultArgument[] { new LiteralArgument("expression"), new LiteralArgument("4") }, CultureInfo.InvariantCulture, default);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("expr");
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName_With_Function()
    {
        // Arrange
        var parser = new LeftExpressionParser();
        var functionParseResult = new FunctionParseResult("Left", new FunctionParseResultArgument[] { new LiteralArgument("expression"), new FunctionArgument(new FunctionParseResult("MyFunction", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, default)) }, CultureInfo.InvariantCulture, default);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("expr");
    }
}
