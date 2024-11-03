namespace ExpressionFramework.Parser.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly IServiceScope _scope;
    private readonly IFunctionResultParser _functionResultParserMock = Substitute.For<IFunctionResultParser>();

    public IntegrationTests()
    {
        _functionResultParserMock.Parse(Arg.Any<FunctionParseResult>(), Arg.Any<object?>(), Arg.Any<IFunctionParseResultEvaluator>(), Arg.Any<IExpressionParser>())
            .Returns(x => x.ArgAt<FunctionParseResult>(0).FunctionName == "MyPredicate"
                ? Result.Success<object?>(x[1] is int i && i > 2)
                : Result.Continue<object?>());

        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .AddSingleton(_functionResultParserMock)
            .BuildServiceProvider(true);

        _scope = _provider.CreateScope();
    }
    
    [Fact]
    public void Can_Parse_Function_With_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=CONTEXT()", CultureInfo.InvariantCulture, "Hello world");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Parse_Function_With_StringFind_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=StringFind(\"Hello\", \"e\")", CultureInfo.InvariantCulture);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Can_Parse_Function_With_Expression_And_Using_FormattableStrings_As_Well()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=CONSTANT(@\"Hello world\")", CultureInfo.InvariantCulture, _scope.ServiceProvider.GetRequiredService<IFormattableStringParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Parse_Function_With_Nested_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=LEFT(CONTEXT(), 5)", CultureInfo.InvariantCulture, "Hello world!");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void Can_Parse_Function_Without_Nested_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=LEFT(\"Hello world!\", 5)", CultureInfo.InvariantCulture);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void Can_Parse_Function_With_Context()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=Aggregate(Context(),AddAggregator())", CultureInfo.InvariantCulture, new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Can_Parse_Function_With_Context_And_Operator()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=Context() == \"Hello\"", CultureInfo.InvariantCulture, "Hello");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Can_Parse_Function_With_Generated_DefaultValue_On_Nullable_Property()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=FirstOrDefault(Context(),MyPredicate())", CultureInfo.InvariantCulture, new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Parse_Function_With_Supplied_DefaultValue_On_Nullable_Property()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("= FirstOrDefault(Context(), MyPredicate(), 13)", CultureInfo.InvariantCulture, new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(13);
    }

    [Fact]
    public void Can_Parse_Function_With_Correct_Typed_Arguments()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=Constant(13)", CultureInfo.InvariantCulture);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(13);
    }

    [Fact]
    public void Function_With_String_Argument_Preserves_WhiteSpace()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=ToUpperCase(\"  space  \")", CultureInfo.InvariantCulture);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("  SPACE  ");
    }

    [Fact]
    public void Unknown_Expression_Gives_NotSupported()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();

        // Act
        var result = parser.Parse("=UNKNOWN()", CultureInfo.InvariantCulture, this);

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    public void Dispose()
    {
        _scope.Dispose();
        _provider.Dispose();
    }
}
