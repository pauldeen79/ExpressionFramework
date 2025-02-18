namespace ExpressionFramework.Parser.Tests;

public sealed class ExpressionFrameworkParserTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly IServiceScope _scope;

    public ExpressionFrameworkParserTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .BuildServiceProvider(true);

        _scope = _provider.CreateScope();
    }

    [Fact]
    public void ParseExpression_Returns_NotSupported_When_Expression_Is_Unknown()
    {
        // Arrange
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Unknown").Build(), _scope.ServiceProvider.GetRequiredService<IFunctionEvaluator>(), _scope.ServiceProvider.GetRequiredService<IExpressionEvaluator>(), new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = ParseExpression(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void ParseExpression_Returns_Success_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Alright()
    {
        // Arrange
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Context").Build(), _scope.ServiceProvider.GetRequiredService<IFunctionEvaluator>(), _scope.ServiceProvider.GetRequiredService<IExpressionEvaluator>(), new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = ParseExpression(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ContextExpression>();
    }

    [Fact]
    public void ParseExpression_Returns_Invalid_With_Expression_When_Expression_Is_Known_And_Arguments_Are_Not_Alright()
    {
        // Arrange
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Delegate").Build(), _scope.ServiceProvider.GetRequiredService<IFunctionEvaluator>(), _scope.ServiceProvider.GetRequiredService<IExpressionEvaluator>(), new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = ParseExpression(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Missing argument: Value");
    }

    [Fact]
    public void Can_ParseExpression_Typed_Expression()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("TypedConstant<System.String>")
            .AddArguments(new ConstantArgumentBuilder().WithValue("Test"))
            .Build(), _scope.ServiceProvider.GetRequiredService<IFunctionEvaluator>(), _scope.ServiceProvider.GetRequiredService<IExpressionEvaluator>(), new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = Parse<string>(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<ITypedExpression<string>>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _provider.Dispose();
    }

    private Result<Expression> ParseExpression(FunctionCallContext context) => _provider
        .GetRequiredService<IExpressionFrameworkParser>()
        .ParseExpression(context);

    private Result<ITypedExpression<T>> Parse<T>(FunctionCallContext context) => _provider
        .GetRequiredService<IExpressionFrameworkParser>()
        .ParseExpression<T>(context);
}
