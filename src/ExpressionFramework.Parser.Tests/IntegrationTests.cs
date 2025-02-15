namespace ExpressionFramework.Parser.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly IServiceScope _scope;

    public IntegrationTests()
    {
        _provider = new ServiceCollection()
            .AddParsers()
            .AddExpressionParser()
            .AddScoped<IFunction, MyFunction>()
            .BuildServiceProvider(true);

        _scope = _provider.CreateScope();
    }

    [Fact]
    public void Can_Evaluate_Function_With_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=CONTEXT()", new ExpressionStringEvaluatorSettingsBuilder(), "Hello world");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Function_With_Expression_And_Using_FormattableStrings_As_Well()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=CONSTANT(@\"Hello world\")", new ExpressionStringEvaluatorSettingsBuilder(), _scope.ServiceProvider.GetRequiredService<IFormattableStringParser>());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Function_With_Nested_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=LEFT(CONTEXT(), 5)", new ExpressionStringEvaluatorSettingsBuilder(), "Hello world!");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void Can_Evaluate_Function_Without_Nested_Expression()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=LEFT(\"Hello world!\", 5)", new ExpressionStringEvaluatorSettingsBuilder());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello");
    }

    [Fact]
    public void Can_Evaluate_Function_With_Context()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=Aggregate(Context(),AddAggregator())", new ExpressionStringEvaluatorSettingsBuilder(), new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Can_Evaluate_Function_With_Context_And_Operator()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=Context() == \"Hello\"", new ExpressionStringEvaluatorSettingsBuilder(), "Hello");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void Can_Evaluate_Function_With_Generated_DefaultValue_On_Nullable_Property()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=FirstOrDefault(Context(),MyPredicate())", new ExpressionStringEvaluatorSettingsBuilder(), new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Evaluate_Function_With_Supplied_DefaultValue_On_Nullable_Property()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("= FirstOrDefault(Context(), MyPredicate(), 13)", new ExpressionStringEvaluatorSettingsBuilder(), new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(13);
    }

    [Fact]
    public void Can_Evaluate_Function_With_Correct_Typed_Arguments()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=Constant(13)", new ExpressionStringEvaluatorSettingsBuilder());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(13);
    }

    [Fact]
    public void Function_With_String_Argument_Preserves_Whitespace()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=ToUpperCase(\"  space  \")", new ExpressionStringEvaluatorSettingsBuilder());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("  SPACE  ");
    }

    [Fact]
    public void Unknown_Expression_Gives_Invalid()
    {
        // Arrange
        var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();

        // Act
        var result = parser.Evaluate("=UNKNOWN()", new ExpressionStringEvaluatorSettingsBuilder(), this);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    public void Dispose()
    {
        _scope.Dispose();
        _provider.Dispose();
    }

    [FunctionName("MyPredicate")]
    [FunctionArgument("Expression", typeof(object), false)]
    private sealed class MyFunction : IFunction
    {
        public Result<object?> Evaluate(FunctionCallContext context)
        {
            return Result.Success<object?>(context.GetArgumentValueResult(0, "Expression", null).GetValue() is int i && i > 2);
        }
    }
}
