namespace ExpressionFramework.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection services)
        => services
            .AddSingleton<IFunction, AggregateFunction>()
            .AddSingleton<IFunction, AddAggregatorFunction>()
            .AddSingleton<IFunction, ComposableEvaluatableFunction>();
}
