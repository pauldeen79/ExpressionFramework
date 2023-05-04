namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public sealed class ComposedEvaluatableParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public ComposedEvaluatableParserTests()
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
        var parser = new ComposedEvaluatableParser();
        var contextValue = new[] { new ComposableEvaluatable("1", new EqualsOperator(), "2") };
        var functionParseResult = new FunctionParseResult("ComposedEvaluatable", new FunctionParseResultArgument[]
        {
            new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue))
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ComposedEvaluatable>();
    }

    [Fact]
    public void Parse_Returns_Failure_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new ComposedEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("ComposedEvaluatable", new FunctionParseResultArgument[]
        {
            new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue))
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Conditions is not of type System.Collections.Generic.IEnumerable`1[[ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable, ExpressionFramework.Domain.Specialized, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]");
    }

    public void Dispose() => _provider.Dispose();
}
