namespace ExpressionFramework.Domain.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IConditionEvaluator, ConditionEvaluator>()
            .AddSingleton<IExpressionEvaluator, ExpressionEvaluator>()
            .AddSingleton<IValueProvider, ValueProvider>()
            .AddExpressionHandlers();
}
