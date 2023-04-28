namespace ExpressionFramework.Parser.Tests;

public sealed class ExpressionFrameworkParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public ExpressionFrameworkParserTests()
    {
        _provider = new ServiceCollection().AddParsers().AddExpressionParser().BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_NotSupported_When_Expression_Is_Unknown()
    {
        // Act
        var result = _provider.GetRequiredService<IExpressionFrameworkParser>().Parse(new FunctionParseResult("Unknown", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null), _provider.GetRequiredService<IFunctionParseResultEvaluator>());

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    
    [Fact]
    public void Parse_Returns_Success_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Alright()
    {
        // Act
        var result = _provider.GetRequiredService<IExpressionFrameworkParser>().Parse(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null), _provider.GetRequiredService<IFunctionParseResultEvaluator>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ContextExpression>();
    }

    [Fact]
    public void Parse_Returns_Invalid_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Not_Alright()
    {
        // Act
        var result = _provider.GetRequiredService<IExpressionFrameworkParser>().Parse(new FunctionParseResult("Constant", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null), _provider.GetRequiredService<IFunctionParseResultEvaluator>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Missing argument: Value");
    }

    public void Dispose() => _provider.Dispose();
}
