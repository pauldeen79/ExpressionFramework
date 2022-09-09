﻿namespace ExpressionFramework.Domain.Extensions;

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
            .AddSingleton<IExpressionHandler, SwitchExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToLowerCaseExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToPascalCaseExpressionHandler>()
            .AddSingleton<IExpressionHandler, ToUpperCaseExpressionHandler>()
            .AddSingleton<IOperatorHandler, ContainsOperatorHandler>()
            .AddSingleton<IOperatorHandler, EndsWithOperatorHandler>()
            .AddSingleton<IOperatorHandler, EqualsOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsGreaterOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsGreaterOrEqualOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNotNullOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNotNullOrEmptyOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNotNullOrWhiteSpaceOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNullOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNullOrEmptyOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsNullOrWhiteSpaceOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsSmallerOperatorHandler>()
            .AddSingleton<IOperatorHandler, IsSmallerOrEqualOperatorHandler>()
            .AddSingleton<IOperatorHandler, NotContainsOperatorHandler>()
            .AddSingleton<IOperatorHandler, NotEndsWithOperatorHandler>()
            .AddSingleton<IOperatorHandler, NotEqualsOperatorHandler>()
            .AddSingleton<IOperatorHandler, NotStartsWithOperatorHandler>()
            .AddSingleton<IOperatorHandler, StartsWithOperatorHandler>();
}
