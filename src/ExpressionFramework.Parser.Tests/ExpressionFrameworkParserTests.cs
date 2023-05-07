namespace ExpressionFramework.Parser.Tests;

public sealed class ExpressionFrameworkParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    
    public ExpressionFrameworkParserTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_NotSupported_When_Expression_Is_Unknown()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Unknown").Build();

        // Act
        var result = Parse(functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Parse_Returns_Success_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Alright()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Context").Build();

        // Act
        var result = Parse(functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ContextExpression>();
    }

    [Fact]
    public void Parse_Returns_Invalid_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Not_Alright()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Delegate").Build();

        // Act
        var result = Parse(functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Missing argument: Value");
    }

    [Fact]
    public void Can_Parse_Typed_Expression()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("TypedConstant<System.String>")
            .AddArguments(new LiteralArgumentBuilder().WithValue("Test"))
            .Build();

        // Act
        var result = Parse<string>(functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<ITypedExpression<string>>();
    }

    public void Dispose() => _provider.Dispose();

    private Result<Expression> Parse(FunctionParseResult functionParseResult) => _provider
        .GetRequiredService<IExpressionFrameworkParser>()
        .Parse(
            functionParseResult,
            _provider.GetRequiredService<IFunctionParseResultEvaluator>(),
            _provider.GetRequiredService<IExpressionParser>());

    private Result<ITypedExpression<T>> Parse<T>(FunctionParseResult functionParseResult) => _provider
        .GetRequiredService<IExpressionFrameworkParser>()
        .Parse<T>(
            functionParseResult,
            _provider.GetRequiredService<IFunctionParseResultEvaluator>(),
            _provider.GetRequiredService<IExpressionParser>());
}
