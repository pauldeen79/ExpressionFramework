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
                x.TryAddSingleton<IConditionEvaluatorProvider, ConditionEvaluatorProvider>();
                if (!x.Any(y => y.ImplementationType == typeof(AggregateExpressionEvaluatorHandler)))
                {
                    x.AddSingleton<IExpressionEvaluatorHandler, AggregateExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, ConditionalExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, ChainedExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, ConstantExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, ContextExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, DelegateExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, EmptyExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, FieldExpressionEvaluatorHandler>();
                    x.AddSingleton<IExpressionEvaluatorHandler, SwitchEpressionEvaluationHandler>();
                }
                x.TryAddSingleton<IValueProvider, ValueProvider>();
                if (!x.Any(y => y.ImplementationType == typeof(ContainsFunctionEvaluator)))
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
                if (!x.Any(y => y.ImplementationType == typeof(DivideAggregateFunctionEvaluator)))
                {
                    x.AddSingleton<IAggregateFunctionEvaluator, DivideAggregateFunctionEvaluator>();
                    x.AddSingleton<IAggregateFunctionEvaluator, EmptyAggegateFunctionEvaluator>();
                    x.AddSingleton<IAggregateFunctionEvaluator, FirstAggregateFunctionEvaluator>();
                    x.AddSingleton<IAggregateFunctionEvaluator, MinusAggregateFunctionEvaluator>();
                    x.AddSingleton<IAggregateFunctionEvaluator, MultiplyAggregateFunctionEvaluator>();
                    x.AddSingleton<IAggregateFunctionEvaluator, PlusAggregateFunctionEvaluator>();
                }
            });
}
