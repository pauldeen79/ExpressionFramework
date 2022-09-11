namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreEntities : ExpressionFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.template.generated.cs";

    public override object CreateModel()
        => GetImmutableClasses
        (
            GetCoreModels(),
            "ExpressionFramework.Domain"
        );
}
