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
        var result = parser.Parse("=CONTEXT()", CultureInfo.InvariantCulture, "Hello world");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Parse_Function_With_Expression_And_Using_FormattableStrings_As_Well()
    {
        // Act
        var parser = _provider.GetRequiredService<IExpressionStringParser>();
        var result = parser.Parse("=CONSTANT(@\"Hello world\")", CultureInfo.InvariantCulture);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Parse_Function_With_Nested_Expression()
    {
        // Act
        var parser = _provider.GetRequiredService<IExpressionStringParser>();
        var result = parser.Parse("=LEFT(CONTEXT(), 5)", CultureInfo.InvariantCulture, "Hello world!");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void Unknown_Expression_Gives_NotSupported()
    {
        // Act
        var parser = _provider.GetRequiredService<IExpressionStringParser>();
        var result = parser.Parse("=UNKNOWN()", CultureInfo.InvariantCulture, this);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    public void Dispose() => _provider.Dispose();
}
