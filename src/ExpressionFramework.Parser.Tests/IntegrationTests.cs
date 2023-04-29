namespace ExpressionFramework.Parser.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _provider;

    public IntegrationTests()
    {
        _provider = new ServiceCollection().AddParsers().AddExpressionParser().BuildServiceProvider();
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

    [Theory(Skip = "Not seeing code coverage if build fails"), MemberData(nameof(AllParsers))]
    public void All_Resolvers_Should_Return_An_Expression(IExpressionResolver resolver)
    {
        // Act
        var result = (Result<Expression>)resolver.GetType()
            .GetMethod("DoParse", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(resolver, new object[]
            {
                new FunctionParseResult("SomeFunction", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null),
                _provider.GetRequiredService<IFunctionParseResultEvaluator>()
            })!;

        // Assert
        result.Should().NotBeNull();
    }


    public static IEnumerable<object[]> AllParsers()
        => typeof(ExpressionFrameworkParser).Assembly.GetExportedTypes()
            .Where(t => !t.IsAbstract && typeof(IExpressionResolver).IsAssignableFrom(t))
            .Select(t => new object[] { (IExpressionResolver)Activator.CreateInstance(t, new Mock<IExpressionParser>().Object)! });

    public void Dispose() => _provider.Dispose();
}
