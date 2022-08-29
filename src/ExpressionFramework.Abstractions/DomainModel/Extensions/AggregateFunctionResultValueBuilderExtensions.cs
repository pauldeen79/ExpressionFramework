namespace ExpressionFramework.Abstractions.DomainModel.Extensions;

public static partial class AggregateFunctionResultValueBuilderExtensions
{
    public static T Stop<T>(this T instance)
        where T : IAggregateFunctionResultValueBuilder
        => instance.WithContinue(false);
}
