namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public sealed class AggregateExpressionParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly Mock<IFunctionResultParser> _functionResultParserMock = new();

    public AggregateExpressionParserTests()
    {
        _functionResultParserMock.Setup(x => x.Parse(It.IsAny<FunctionParseResult>(), It.IsAny<object?>(), It.IsAny<IFunctionParseResultEvaluator>(), It.IsAny<IExpressionParser>()))
            .Returns<FunctionParseResult, object?, IFunctionParseResultEvaluator, IExpressionParser>((result, context, evaluator, parser) => result.FunctionName == "MyArray"
            ? Result<object?>.Success(new[] { 1, 2 })
            : Result<object?>.Continue());

        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .AddSingleton(_functionResultParserMock.Object)
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_Success_When_GetArgumentValue_Succeeds()
    {
        // Arrange
        var functionParseResult = _provider.GetRequiredService<IFunctionParser>().Parse("Aggregate(MyArray(),AddAggregator())", CultureInfo.InvariantCulture);

        // Act
        var result = CreateSut().Parse(functionParseResult.GetValueOrThrow(), null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Parse_Returns_Success_When_GetArgumentValue_Succeeds_With_Context()
    {
        // Arrange
        var functionParseResult = _provider.GetRequiredService<IFunctionParser>().Parse("Aggregate(Context(),AddAggregator())", CultureInfo.InvariantCulture, new[] { 1, 2 });

        // Act
        var result = CreateSut().Parse(functionParseResult.GetValueOrThrow(), _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>()).GetValueOrThrow().Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Parse_Returns_Failure_When_ArgumentParsing_Fails()
    {
        // Arrange
        var functionParseResult = _provider.GetRequiredService<IFunctionParser>().Parse("Aggregate(MyArray(),UnknownAggregator())", CultureInfo.InvariantCulture);

        // Act
        var result = CreateSut().Parse(functionParseResult.GetValueOrThrow(), null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unknown function found: UnknownAggregator");
    }

    private static AggregateExpressionParser CreateSut() => new AggregateExpressionParser();

    public void Dispose() => _provider.Dispose();
}
