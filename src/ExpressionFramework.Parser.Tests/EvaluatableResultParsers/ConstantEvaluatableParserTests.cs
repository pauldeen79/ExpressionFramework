namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public sealed class ConstantEvaluatableParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public ConstantEvaluatableParserTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new ConstantEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Wrong").WithContext(contextValue).Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName_And_Arguments()
    {
        // Arrange
        var parser = new ConstantEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("ConstantEvaluatable")
            .AddArguments(new LiteralArgumentBuilder().WithValue("true"))
            .WithContext(contextValue)
            .Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantEvaluatable>();
    }

    [Fact]
    public void Parse_Returns_Failure_For_Correct_FunctionName_But_Wrong_Arguments()
    {
        // Arrange
        var parser = new ConstantEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("ConstantEvaluatable")
            .AddArguments(new LiteralArgumentBuilder().WithValue("12"))
            .WithContext(contextValue)
            .Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Value is not of type System.Boolean");
    }

    public void Dispose() => _provider.Dispose();

    private Result<object?> Parse(ConstantEvaluatableParser parser, FunctionParseResult functionParseResult)
        => parser.Parse
        (
            functionParseResult,
            null,
            _provider.GetRequiredService<IFunctionParseResultEvaluator>(),
            _provider.GetRequiredService<IExpressionParser>()
        );
}
