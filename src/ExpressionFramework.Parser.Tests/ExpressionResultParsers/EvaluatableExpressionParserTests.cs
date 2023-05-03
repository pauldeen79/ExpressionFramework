namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public sealed class EvaluatableExpressionParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public EvaluatableExpressionParserTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .BuildServiceProvider();
    }

    [Fact]
    public void Parse_Returns_Success_When_Arguments_Are_Valid()
    {
        // Arrange
        var parser = new EvaluatableExpressionParser();
        var contextValue = new SingleEvaluatable(new ContextExpression(), new EqualsOperator(), "some value");
        var functionParseResult = new FunctionParseResult("Evaluatable", new FunctionParseResultArgument[]
        {
            new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)),
            new LiteralArgument("some value"),
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<EvaluatableExpression>();
        ((EvaluatableExpression)result.Value!).Expression.Should().BeOfType<ConstantResultExpression>();
        ((ConstantResultExpression)((EvaluatableExpression)result.Value!).Expression).Value.Should().BeOfType<Result<object?>>();
        ((Result<object?>)((ConstantResultExpression)((EvaluatableExpression)result.Value!).Expression).Value).Value.Should().BeEquivalentTo("some value");
        ((EvaluatableExpression)result.Value!).Condition.Should().BeSameAs(contextValue);
    }

    [Fact]
    public void Parse_Returns_Failure_When_Arguments_Are_Not_Valid()
    {
        // Arrange
        var parser = new EvaluatableExpressionParser();
        var contextValue = new SingleEvaluatable(new ContextExpression(), new EqualsOperator(), "some value");
        var functionParseResult = new FunctionParseResult("Evaluatable", new FunctionParseResultArgument[]
        {
            new FunctionArgument(new FunctionParseResult("Unknown_Function", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)),
            new LiteralArgument("some value"),
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.ErrorMessage.Should().Be("Unknown function found: Unknown_Function");
    }

    public void Dispose() => _provider.Dispose();
}
