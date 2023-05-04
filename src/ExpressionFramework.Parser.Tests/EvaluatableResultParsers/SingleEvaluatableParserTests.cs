namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public sealed class SingtleEvaluatableParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public SingtleEvaluatableParserTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName_And_Arguments()
    {
        // Arrange
        var parser = new SingleEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("SingleEvaluatable", new FunctionParseResultArgument[]
        {
            new LiteralArgument("1"),
            new FunctionArgument(new FunctionParseResult("EqualsOperator", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)),
            new LiteralArgument("2"),
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<SingleEvaluatable>();
    }

    [Fact]
    public void Parse_Returns_Failure_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new SingleEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("SingleEvaluatable", new FunctionParseResultArgument[]
        {
            new LiteralArgument("1"),
            new LiteralArgument("some unknown operator"),
            new LiteralArgument("2"),
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Operator is not of type ExpressionFramework.Domain.Operator");
    }

    public void Dispose() => _provider.Dispose();
}
