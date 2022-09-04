namespace ExpressionFramework.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IConditionEvaluator, ConditionEvaluator>()
            .AddSingleton<IConditionEvaluatorProvider, ConditionEvaluatorProvider>()
            .AddSingleton<IExpressionEvaluator, ExpressionEvaluator>()
            .AddSingleton<IExpressionHandler, ConstantExpressionHandler>()
            .AddSingleton<IExpressionHandler, EmptyExpressionHandler>()
            .AddSingleton<IOperatorHandler, EqualsOperatorHandler>();
}
