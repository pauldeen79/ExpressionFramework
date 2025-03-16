namespace ExpressionFramework.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection services)
        => services
            // Evaluatables
            .AddSingleton<IFunction, ComposableEvaluatableFunction>()
            .AddSingleton<IFunction, ComposedEvaluatableFunction>()
            .AddSingleton<IFunction, ConstantEvaluatableFunction>()
            .AddSingleton<IFunction, ConstantResultEvaluatableFunction>()
            .AddSingleton<IFunction, DelegateEvaluatableFunction>()
            .AddSingleton<IFunction, DelegateResultEvaluatableFunction>()
            .AddSingleton<IFunction, OperatorEvaluatableFunction>()
            // Operators
            .AddSingleton<IFunction, EndsWithOperatorFunction>()
            .AddSingleton<IFunction, EnumerableContainsOperatorFunction>()
            .AddSingleton<IFunction, EnumerableNotContainsOperatorFunction>()
            .AddSingleton<IFunction, EqualsOperatorFunction>()
            .AddSingleton<IFunction, IsGreaterOperatorFunction>()
            .AddSingleton<IFunction, IsGreaterOrEqualOperatorFunction>()
            .AddSingleton<IFunction, IsNotNullOperatorFunction>()
            .AddSingleton<IFunction, IsNotNullOrEmptyOperatorFunction>()
            .AddSingleton<IFunction, IsNotNullOrWhiteSpaceOperatorFunction>()
            .AddSingleton<IFunction, IsNullOperatorFunction>()
            .AddSingleton<IFunction, IsNullOrEmptyOperatorFunction>()
            .AddSingleton<IFunction, IsNullOrWhiteSpaceOperatorFunction>()
            .AddSingleton<IFunction, IsSmallerOperatorFunction>()
            .AddSingleton<IFunction, IsSmallerOrEqualOperatorFunction>()
            .AddSingleton<IFunction, NotEndsWithOperatorFunction>()
            .AddSingleton<IFunction, NotEqualsOperatorFunction>()
            .AddSingleton<IFunction, NotStartsWithOperatorFunction>()
            .AddSingleton<IFunction, StartsWithOperatorFunction>()
            .AddSingleton<IFunction, StringContainsOperatorFunction>()
            .AddSingleton<IFunction, StringNotContainsOperatorFunction>();
}
