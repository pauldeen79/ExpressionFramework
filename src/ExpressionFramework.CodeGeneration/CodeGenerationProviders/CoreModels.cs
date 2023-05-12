namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreModels : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetCoreModels(),
            Constants.Namespaces.Domain,
            Constants.Namespaces.DomainModels);
}
