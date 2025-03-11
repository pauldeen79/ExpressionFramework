namespace ExpressionFramework.Core.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IServiceScope _scope;

    public IntegrationTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddParsers()
            .AddExpressionFramework()
            .BuildServiceProvider();

        _scope = _serviceProvider.CreateScope();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _serviceProvider.Dispose();
    }

    [Fact]
    public void Can_Parse_ExpressionString_With_ExpressionFramework_Function()
    {
        // Arrange
        var evaluator = _scope.ServiceProvider.GetRequiredService<IExpressionStringEvaluator>();
        var settings = new ExpressionStringEvaluatorSettingsBuilder();
        var context = new[] { 1, 2, 3 };

        // Act
        var result = evaluator.Evaluate("=Aggregate(context, AddAggregator(), null)", settings, context);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 + 2 + 3);
    }
}
