namespace ExpressionFramework.Domain.Tests.Support;

[Binding]
public static class BuilderFactoryInitializationHook
{
    [BeforeTestRun]
    public static void SetupUnknownTypes()
    {
        ExpressionBuilderFactory.Register(typeof(UnknownExpression), _ => new UnknownExpressionBuilder());
        OperatorBuilderFactory.Register(typeof(UnknownOperator), _ => new UnknownOperatorBuilder());
    }
}
