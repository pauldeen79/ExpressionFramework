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
        var functionParseResult = new FunctionParseResult("ComposableEvaluatable", new FunctionParseResultArgument[]
        {
            new LiteralArgument("1"),
            new FunctionArgument(new FunctionParseResult("EqualsOperator", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)),
            new LiteralArgument("2"),
            new LiteralArgument(Combination.Or.ToString())
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ComposableEvaluatable>();
        var composableEvaluatable = (ComposableEvaluatable)result.Value!;
        composableEvaluatable.LeftExpression.Should().BeOfType<ConstantExpression>();
        ((ConstantExpression)composableEvaluatable.LeftExpression).Value.Should().BeEquivalentTo(1);
        composableEvaluatable.RightExpression.Should().BeOfType<ConstantExpression>();
        ((ConstantExpression)composableEvaluatable.RightExpression).Value.Should().BeEquivalentTo(2);
        composableEvaluatable.Operator.Should().BeOfType<EqualsOperator>();
        composableEvaluatable.Combination.Should().Be(Combination.Or);
        composableEvaluatable.StartGroup.Should().BeFalse();
        composableEvaluatable.EndGroup.Should().BeFalse();
    }

    [Fact]
    public void Parse_Returns_Failure_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new ComposableEvaluatableParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("ComposableEvaluatable", new FunctionParseResultArgument[]
        {
            new LiteralArgument("1"),
            new FunctionArgument(new FunctionParseResult("EqualsOperator", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue)),
            new LiteralArgument("2"),
            new LiteralArgument("some unknown combination")
        }, CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _provider.GetRequiredService<IFunctionParseResultEvaluator>(), _provider.GetRequiredService<IExpressionParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Combination value [some unknown combination] could not be converted to ExpressionFramework.Domain.Domains.Combination. Error message: Requested value 'some unknown combination' was not found.");
    }

    public void Dispose() => _provider.Dispose();
}
