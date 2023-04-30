namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public sealed class AggregateExpressionParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly Mock<IFunctionResultParser> _functionResultParserMock = new();

    public AggregateExpressionParserTests()
    {
        _functionResultParserMock.Setup(x => x.Parse(It.IsAny<FunctionParseResult>(), It.IsAny<object?>(), It.IsAny<IFunctionParseResultEvaluator>()))
            .Returns<FunctionParseResult, object?, IFunctionParseResultEvaluator>((result, context, evaluator) => result.FunctionName == "MyArray"
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
        var result = CreateSut().Parse(functionParseResult.GetValueOrThrow(), null, _provider.GetRequiredService<IFunctionParseResultEvaluator>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    private AggregateExpressionParser CreateSut()
        => new AggregateExpressionParser(_provider.GetRequiredService<IExpressionParser>());

    public void Dispose() => _provider.Dispose();
}
