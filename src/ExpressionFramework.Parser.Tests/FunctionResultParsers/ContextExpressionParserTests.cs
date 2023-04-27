using CrossCutting.Utilities.Parsers;

namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public class ContextExpressionParserTests
{
    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new ContextExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, default);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new ContextExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, default);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(contextValue);
    }
}
