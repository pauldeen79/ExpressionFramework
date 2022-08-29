namespace ExpressionFramework.Abstractions.DomainModel.Extensions;

public static partial class CaseBuilderExtensions
{
    public static T When<T>(this T instance, IConditionBuilder conditionBuilder)
        where T : ICaseBuilder
        => instance.AddConditions(conditionBuilder);

    public static T Then<T>(this T instance, IExpressionBuilder expression)
        where T : ICaseBuilder
        => instance.WithExpression(expression);
}
