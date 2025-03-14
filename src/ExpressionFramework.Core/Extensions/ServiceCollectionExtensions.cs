namespace ExpressionFramework.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection services)
        => services
            .AddSingleton<IFunction, ComposableEvaluatableFunction>()
            .AddSingleton<IFunction, ComposedEvaluatableFunction>()
            .AddSingleton<IFunction, ConstantEvaluatableFunction>()
            .AddSingleton<IFunction, ConstantResultEvaluatableFunction>()
            .AddSingleton<IFunction, DelegateEvaluatableFunction>()
            .AddSingleton<IFunction, DelegateResultEvaluatableFunction>()
            .AddSingleton<IFunction, OperatorEvaluatableFunction>();
}
