namespace ExpressionFramework.Parser.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public IntegrationTests()
    {
        _provider = new ServiceCollection().AddParsers().AddExpressionParsers().BuildServiceProvider();
    }
    
    [Fact]
    public void Can_Parse_Function_With_Expression()
    {
        // Act
        var parser = _provider.GetRequiredService<IExpressionStringParser>();
        var result = parser.Parse("=CONTEXT()", CultureInfo.InvariantCulture, this);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
    }

    [Fact]
    public void Unknown_Expression_Gives_NotSupported()
    {
        // Act
        var parser = _provider.GetRequiredService<IExpressionStringParser>();
        var result = parser.Parse("=CONTEXT()", CultureInfo.InvariantCulture, this);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    public void Dispose() => _provider.Dispose();
}
