namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public sealed class CompoundExpressionParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();

    public CompoundExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
              .Returns<FunctionParseResult, IExpressionParser, object?>((result, parser, context) => result.FunctionName switch
              {
                  "MyFunction" => Result<object?>.Success(new AddAggregator()),
                  "Context" => Result<object?>.Success(context),
                  _ => Result<object?>.NotSupported("Only Parsed result function is supported")
              });

        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .AddSingleton(_evaluatorMock.Object)
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_Success_When_GetArgumentValue_Succeeds()
    {
        // Arrange
        var functionParseResult = _provider.GetRequiredService<IFunctionParser>().Parse("Compound(1,2,MyFunction())", CultureInfo.InvariantCulture);

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
        var functionParseResult = _provider.GetRequiredService<IFunctionParser>().Parse("Compound(Context(),2,MyFunction())", CultureInfo.InvariantCulture, 1);

        // Act
        var result = CreateSut().Parse(functionParseResult.GetValueOrThrow(), _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>()).GetValueOrThrow().Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    private static CompoundExpressionParser CreateSut() => new CompoundExpressionParser();

    public void Dispose() => _provider.Dispose();
}
