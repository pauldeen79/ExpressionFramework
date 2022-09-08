namespace ExpressionFramework.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IConditionEvaluator, ConditionEvaluator>()
            .AddSingleton<IConditionEvaluatorProvider, ConditionEvaluatorProvider>()
            .AddSingleton<IExpressionEvaluator, ExpressionEvaluator>()
            .AddSingleton<IValueProvider, ValueProvider>()
            .AddSingleton<IExpressionHandler, ChainedExpressionHandler>()
            .AddSingleton<IExpressionHandler, ConditionalExpressionHandler>()
            .AddSingleton<IExpressionHandler, ConstantExpressionHandler>()
            .AddSingleton<IExpressionHandler, ContextExpressionHandler>()
            .AddSingleton<IExpressionHandler, DelegateExpressionHandler>()
            .AddSingleton<IExpressionHandler, EmptyExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToLowerCaseExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToPascalCaseExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToUpperCaseExpressionHandler>()
            .AddSingleton<IOperatorHandler, EqualsOperatorHandler>();
}
