namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // not needed for abstract entities, because each derived class will do its own validation

    public override object CreateModel()
        => GetImmutableClasses(GetAbstractModels(), "ExpressionFramework.Domain");
}
