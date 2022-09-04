namespace ExpressionFramework.SpecFlow.Tests.Support;

[Binding]
public class ExpressionValueRetriever : IValueRetriever
{
    [BeforeTestRun]
    public static void RegisterValueRetrievers()
        => Service.Instance.ValueRetrievers.Register<ExpressionValueRetriever>();

    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        => propertyType == typeof(Expression);

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        => new ConstantExpression(StringExpression.Evaluate(keyValuePair.Value));
}
