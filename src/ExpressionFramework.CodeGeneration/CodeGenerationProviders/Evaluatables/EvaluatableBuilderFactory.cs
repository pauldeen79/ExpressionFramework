namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class EvaluatableBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(IEvaluatable)),
            new(
                Constants.Namespaces.DomainBuilders,
                nameof(EvaluatableBuilderFactory),
                Constants.TypeNames.Evaluatable,
                Constants.Namespaces.DomainBuildersEvaluatables,
                Constants.Types.EvaluatableBuilder,
                Constants.Namespaces.DomainEvaluatables
            )
        );
}
