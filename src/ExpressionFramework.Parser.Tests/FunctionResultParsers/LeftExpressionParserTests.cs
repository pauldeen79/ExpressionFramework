namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public sealed class LeftExpressionParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();

    public LeftExpressionParserTests()
    {
        _provider = new ServiceCollection().AddParsers().AddExpressionParsers().BuildServiceProvider();
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>()))
                      .Returns<FunctionParseResult>(result => result.FunctionName switch
                       {
                           "MyFunction" => Result<object?>.Success("4"),
                           "Context" => Result<object?>.Success(result.Context),
                           _ => Result<object?>.NotSupported("Only Parsed result function is supported")
                       });
    }

    public void Dispose() => _provider.Dispose();

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new LeftExpressionParser(_provider.GetRequiredService<IExpressionParser>());
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
        var parser = new LeftExpressionParser(_provider.GetRequiredService<IExpressionParser>());
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
        var parser = new LeftExpressionParser(_provider.GetRequiredService<IExpressionParser>());
        var functionParseResult = new FunctionParseResult("Left", new FunctionParseResultArgument[] { new LiteralArgument("expression"), new FunctionArgument(new FunctionParseResult("MyFunction", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, default)) }, CultureInfo.InvariantCulture, default);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("expr");
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName_With_Context_Function()
    {
        // Arrange
        var parser = new LeftExpressionParser(_provider.GetRequiredService<IExpressionParser>());
        var context = "context expression";
        var functionParseResult = new FunctionParseResult("Left", new FunctionParseResultArgument[]
        { 
            new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, context)),
            new FunctionArgument(new FunctionParseResult("MyFunction", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, context))
        }, CultureInfo.InvariantCulture, context);

        // Act
        var result = parser.Parse(functionParseResult, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("cont");
    }
}
