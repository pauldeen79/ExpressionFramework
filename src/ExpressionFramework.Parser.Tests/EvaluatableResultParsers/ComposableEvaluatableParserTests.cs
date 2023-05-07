namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public sealed class ComposableEvaluatableParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public ComposableEvaluatableParserTests()
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
        var parser = new ComposableEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("ComposableEvaluatable")
            .AddArguments(
                new LiteralArgumentBuilder().WithValue("1"),
                new FunctionArgumentBuilder().WithFunction(
                    new FunctionParseResultBuilder()
                        .WithFunctionName("EqualsOperator")
                        .WithContext(contextValue)),
                new LiteralArgumentBuilder().WithValue("2"),
                new LiteralArgumentBuilder().WithValue(Combination.Or.ToString())
            ).WithContext(contextValue).Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ComposableEvaluatable>();
    }

    [Fact]
    public void Parse_Returns_Failure_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new ComposableEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("ComposableEvaluatable")
            .AddArguments(
                new LiteralArgumentBuilder().WithValue("1"),
                new FunctionArgumentBuilder()
                    .WithFunction(
                        new FunctionParseResultBuilder()
                            .WithFunctionName("EqualsOperator")
                            .WithContext(contextValue)),
                new LiteralArgumentBuilder().WithValue("2"),
                new LiteralArgumentBuilder().WithValue("some unknown combination")
            ).WithContext(contextValue).Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Combination value [some unknown combination] could not be converted to ExpressionFramework.Domain.Domains.Combination. Error message: Requested value 'some unknown combination' was not found.");
    }

    private Result<object?> Parse(ComposableEvaluatableParser parser, FunctionParseResult functionParseResult)
        => parser.Parse(
            functionParseResult,
            null,
            _provider.GetRequiredService<IFunctionParseResultEvaluator>(),
            _provider.GetRequiredService<IExpressionParser>());

    public void Dispose() => _provider.Dispose();
}
