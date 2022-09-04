namespace ExpressionFramework.SpecFlow.Tests.Support;

[Binding]
public sealed class ApplicationEntrypoint
{
    private static ServiceProvider? _provider;
    private IServiceScope? _scope;

    [BeforeTestRun]
    public static void SetupApplication()
    {
        _provider = new ServiceCollection()
            .AddExpressionFramework()
            .BuildServiceProvider();
    }

    [AfterTestRun]
    public static void CleanUpApplication()
    {
        if (_provider != null)
        {
            _provider.Dispose();
            _provider = null;
        }
    }

    [BeforeScenario]
    public void SetupScenario()
        => _scope = Provider.CreateScope();

    [AfterScenario]
    public void CleanUpScenario()
    {
        if (_scope != null)
        {
            _scope.Dispose();
            _scope = null;
        }
    }

    private static IServiceProvider Provider
        => _provider == null
            ? throw new InvalidOperationException("Something bad happened, application has not been initialized!")
            : (IServiceProvider)_provider;

    public static IConditionEvaluator ConditionEvaluator
        => Provider.GetRequiredService<IConditionEvaluator>();

    public static IExpressionEvaluator ExpressionEvaluator
        => Provider.GetRequiredService<IExpressionEvaluator>();
}
