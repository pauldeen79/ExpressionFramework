namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public sealed class DelegateEvaluatableParserTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public DelegateEvaluatableParserTests()
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
        var parser = new DelegateEvaluatableParser();
        var contextValue = new Func<bool>(() => true);
        var functionParseResult = new FunctionParseResult("DelegateEvaluatable", new FunctionParseResultArgument[] { new FunctionArgument(new FunctionParseResult("Context", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)) }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<DelegateEvaluatable>();
    }

    [Fact]
    public void Parse_Returns_Failure_For_Correct_FunctionName_But_Wrong_Arguments()
    {
        // Arrange
        var parser = new DelegateEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("DelegateEvaluatable", new FunctionParseResultArgument[] { new LiteralArgument("no Func<Bool>") }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Value is not of type System.Func`1[[System.Boolean, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]");
    }

    public void Dispose() => _provider.Dispose();
}
