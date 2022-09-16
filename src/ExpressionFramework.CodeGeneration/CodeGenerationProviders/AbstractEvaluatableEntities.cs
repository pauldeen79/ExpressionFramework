namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractEvaluatableEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Evaluatable.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableClasses(GetAbstractEvaluatableModels(), "ExpressionFramework.Domain");
}
