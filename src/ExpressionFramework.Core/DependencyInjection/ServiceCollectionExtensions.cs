namespace ExpressionFramework.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExpressionFramework(this IServiceCollection serviceCollection)
        => serviceCollection.AddExpressionFramework(new (_ => { }));

    public static IServiceCollection AddExpressionFramework(this IServiceCollection services,
                                                            Action<IServiceCollection> customConfigurationAction)
        => services
            .With(customConfigurationAction.Invoke)
            .With(x =>
            {
                x.TryAddSingleton<IExpressionEvaluator, ExpressionEvaluator>();
                x.TryAddSingleton<IConditionEvaluator, ConditionEvaluator>();
                if (!x.Any(y => y.ImplementationType == typeof(CompositeExpressionEvaluatorProvider)))
                {
                    x.AddSingleton<IExpressionEvaluatorProvider, CompositeExpressionEvaluatorProvider>();
                    x.AddSingleton<IExpressionEvaluatorProvider, ConstantExpressionEvaluatorProvider>();
                    x.AddSingleton<IExpressionEvaluatorProvider, DelegateExpressionEvaluatorProvider>();
                    x.AddSingleton<IExpressionEvaluatorProvider, EmptyExpressionEvaluatorProvider>();
                    x.AddSingleton<IExpressionEvaluatorProvider, FieldExpressionEvaluatorProvider>();
                }
                x.TryAddSingleton<IValueProvider, ValueProvider>();
                if (!x.Any(y => y.ImplementationType == typeof(CountFunctionEvaluator)))
                {
                    x.AddSingleton<IFunctionEvaluator, ContainsFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, CountFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, DayFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, LeftFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, LengthFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, LowerFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, MonthFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, RightFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, SumFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, TrimFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, UpperFunctionEvaluator>();
                    x.AddSingleton<IFunctionEvaluator, YearFunctionEvaluator>();
                }
                if (!x.Any(y => y.ImplementationType == typeof(DivideCompositeFunctionEvaluator)))
                {
                    x.AddSingleton<ICompositeFunctionEvaluator, DivideCompositeFunctionEvaluator>();
                    x.AddSingleton<ICompositeFunctionEvaluator, EmptyCompositeFunctionEvaluator>();
                    x.AddSingleton<ICompositeFunctionEvaluator, MinusCompositeFunctionEvaluator>();
                    x.AddSingleton<ICompositeFunctionEvaluator, MultiplyCompositeFunctionEvaluator>();
                    x.AddSingleton<ICompositeFunctionEvaluator, PlusCompositeFunctionEvaluator>();
                }
            });
}
