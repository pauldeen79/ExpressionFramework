namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class EvaluatableModelFactory : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IEvaluatable)),
            new(
                Constants.Namespaces.DomainModels,
                nameof(EvaluatableModelFactory),
                Constants.TypeNames.Evaluatable,
                Constants.Namespaces.DomainModelsEvaluatables,
                Constants.Types.EvaluatableModel,
                Constants.Namespaces.DomainEvaluatables
            )
        );
}
