namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class EvaluatableBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IEvaluatable)),
            new(Constants.Namespaces.DomainBuilders,
            nameof(EvaluatableBuilderFactory),
            $"{Constants.Namespaces.Domain}.Evaluatable",
            $"{Constants.Namespaces.DomainBuilders}.Evaluatables",
            "EvaluatableBuilder",
            $"{Constants.Namespaces.Domain}.Evaluatables"));
}
