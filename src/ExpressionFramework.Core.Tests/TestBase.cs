namespace ExpressionFramework.Core.Tests;

public abstract class TestBase
{
    protected IFixture Fixture { get; }

    protected TestBase()
    {
        Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        Fixture.Customizations.Add(new BuilderOmitter());
    }
}

public abstract class TestBase<TSut> : TestBase
{
    protected TSut CreateSut() => Fixture.Create<TSut>();

    protected IFunctionEvaluator FunctionEvaluator { get; } 
    protected IExpressionEvaluator ExpressionEvaluator { get; }

    protected FunctionCallContext CreateFunctionCallContext(FunctionCall functionCall)
        => new FunctionCallContext(functionCall, FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

    protected TestBase()
    {
        FunctionEvaluator = Fixture.Freeze<IFunctionEvaluator>();
        ExpressionEvaluator = Fixture.Freeze<IExpressionEvaluator>();
    }
}

internal sealed class BuilderOmitter : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var propInfo = request as PropertyInfo;
        if (propInfo is not null && propInfo.DeclaringType?.Name.EndsWith("Builder", StringComparison.Ordinal) == true)
        {
            return new OmitSpecimen();
        }

        return new NoSpecimen();
    }
}
